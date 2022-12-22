using comm;
using ComponentFactory.Krypton.Toolkit;
//using DatabaseControl;
using DLMS;
using DLMS.Comm;
using OptocomSoftware.Reporting;
using SEAC.Common;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Controllers;
using SharedCode.Others;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SmartEyeControl_7.Common;
using SmartEyeControl_7.DB;
using SmartEyeControl_7.Reporting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
//using SharedCode.

namespace ucCustomControl
{
    public partial class pnlEvents : UserControl
    {
        #region Definded_Constants

        const string r_EventName = "Event_Name";
        const string r_EventCode = "Event_Code";
        const string r_Caution = "Caution_Number";
        const string r_DisplayCaution = "Display_Caution";
        const string r_FlashTime = "Flash_Time";
        const string r_IsMajorAlarm = "IsMajorAlarm";
        const string r_AlarmStatus = "IsTriggered";
        const string r_ResetAlarmStatus = "ResetAlarmStatus";
        const string r_IsFlash = "isFlash";
        const string r_ReadCaution = "Read_caution";
        const string r_IsEnable = "Is_Enable";
        const string r_IsLogBookEvent = "Is_LogBook_Event";
        const string r_EventDetails = "EventDetails";
        const string r_EventCounter = "EventCounter";
        const string EventDateTimeFormat = "dd MMM-yyyy HH:mm:ss";
        #endregion
        //System.Drawing.Image backImage = System.Drawing.Image.FromFile(Environment.CurrentDirectory + @"\images\backImage.jpg");

        #region DataMembers
        private bool IsEventTimeCompensationRequired = false;
        //private bool IsWebFormat = false;
        //private bool IsWapdaFormat = true;
        bool IsDisplayEventDetail = true;
        bool IsEvenIn_EventCodeList_Start = false; //v4.8.23

        private DLMS_Application_Process Application_Process;
        private ConnectionManager connectionManager;
        private ConnectionController ConnController;
        private ApplicationProcess_Controller AP_Controller;
        private ApplicationController application_Controller;
        private ParameterController Param_Controller;
        private BillingController Instantanous_Controller;
        private LoadProfileController LoadProfile_Controller;

        private EventController Event_Controller;
        private bool ShowtoGUI_Flag;

        List<EventInfo> ListEventInfo;
        List<EventLogInfo> ListEventLogInfo;
        List<EventItem> ListEventItem;

        List<EventData> listEventData;
        Param_EventsCaution[] List_EventsCautions_toGET;

        List<DBConnect.Insert_Evnent_CL_Counter> List_Records = new List<DBConnect.Insert_Evnent_CL_Counter>();
        DBConnect MyDataBase = new DBConnect();
        ds_events Dataset_Events = new ds_events();
        string EventTitle = "";
        int Rpt_to_display = 0; //default report for events
        List<EventData> tempEventLogs = new List<EventData>();
        EventData TData = new EventData();
        Param_Customer_Code obj_CustomerCode;
        Instantaneous_Class Instantaneous_Class_obj;
        //ParamConfigurationSet _ParamConfigurationSet_Object = new ParamConfigurationSet();// Param_Monitoring_Time_Object;
        private long EventReadCount = 0;

        public List<Param_EventsCaution> ListEventCautions;
        public Param_MajorAlarmProfile Param_MajorAlarmProfile_obj = new Param_MajorAlarmProfile();

        List<EventMap> list_eventMap = new List<EventMap>();
        List<EventData> securityReportData = new List<EventData>();

        /// <summary>
        /// Temp leave event members for timebase
        /// </summary>
        BackgroundWorker bgw_ReadLogBook = new BackgroundWorker();
        BackgroundWorker bgw_securityData = new BackgroundWorker();
        ///private BackgroundWorker BckWorkerThread;
        ///private ProgressDialog progressDialog;
        DataBaseController dbController = new DataBaseController();

        //Major Alarms & Event Cautions Grid Columns
        private readonly List<DataGridViewColumn> MajorAlarmGridColumnsAll = null;
        List<DataGridViewColumn> CautionGridColumns = null;
        List<DataGridViewColumn> MajorAlarmGridColumns = null;

        private ApplicationRight RightsUser = null;
        private List<AccessRights> MeterEventRights = null;
        private List<AccessRights> EventRights = null;

        private List<int> EventCodeList_Start = new List<int>();

        bool HidePrintReportButtons;
        #endregion

        #region Properties

        private ParamConfigurationSet _paramConfigurationSet = null;
        internal ParamConfigurationSet ParamConfigurationSet
        {
            get
            {
                if (application_Controller == null ||
                   application_Controller.ParameterConfigurationSet == null)
                    _paramConfigurationSet = null;
                else if (_paramConfigurationSet == null ||
                        _paramConfigurationSet != application_Controller.ParameterConfigurationSet)
                    _paramConfigurationSet = application_Controller.ParameterConfigurationSet;
                return _paramConfigurationSet;
            }
        }

        public ApplicationController Application_Controller
        {
            get
            {
                //if (application_Controller == null)
                //    throw new Exception("Application not Initialized properly");
                return application_Controller;
            }
            set
            {
                if (value != application_Controller)
                {
                    application_Controller = value;
                    Application_Controller.PropertyChanged += new PropertyChangedEventHandler(Application_Controller_PropertyChanged);
                }
            }
        }

        //ToDo:To Link Param_MajorAlarmProfile & ListEventCautions With ParamConfigurationSet
        //public List<Param_EventsCaution> ListEventCautions
        //{
        //    get
        //    {
        //        return ParamConfigurationSet.ParamEventsCaution;
        //    }
        //    set
        //    {
        //        ParamConfigurationSet.ParamEventsCaution = value;
        //    }
        //}
        //public Param_MajorAlarmProfile Param_MajorAlarmProfile_obj
        //{
        //    get
        //    {
        //        return ParamConfigurationSet.ParamMajorAlarmProfile;
        //    }
        //    set
        //    {
        //        ParamConfigurationSet.ParamMajorAlarmProfile = value;
        //    }
        //}

        #endregion

        #region Constructor

        public pnlEvents()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            #region Fill_Event_Code_Start_List
            EventCodeList_Start.Add(113);     //Phase Fail Start
            EventCodeList_Start.Add(114);      // Over Volt Start
            EventCodeList_Start.Add(115);      //Under Volt Start
            EventCodeList_Start.Add(117);      //Reverse Energy Start
            EventCodeList_Start.Add(201);     //OverLoad Start
            EventCodeList_Start.Add(118);    //Reverse Polarity Start
            EventCodeList_Start.Add(121);      //CT Fail
            //Added for Fusion Meter

            #endregion

            MajorAlarmGridColumnsAll = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn col in grid_Events.Columns)
            {
                MajorAlarmGridColumnsAll.Add(col);
            }
            //Initialize All Cautions Grid
            CautionGridColumns = new List<DataGridViewColumn>();
            CautionGridColumns.AddRange(new DataGridViewColumn[] { Caution_Number, Read_caution, Display_Caution, isFlash, Flash_Time });
            //Initialize Major Alarms Grid
            MajorAlarmGridColumns = new List<DataGridViewColumn>();
            MajorAlarmGridColumns.AddRange(new DataGridViewColumn[] { IsMajorAlarm, IsTriggered, ResetAlarmStatus });
        }

        #endregion

        #region Helper_Support_Methods

        public void RefreshEventsConfiguration(Param_MajorAlarmProfile majorAlarmProfile = null,
            List<Param_EventsCaution> eventsCautions = null)
        {
            try
            {
                ListEventInfo = Event_Controller.EventInfoList;
                ListEventLogInfo = Event_Controller.EventLogInfoList;
                ListEventLogInfo.Sort((x, y) => x.EventLogIndex.CompareTo(y.EventLogIndex));
                AddEventItemsToList();
                LoadParams_Events(majorAlarmProfile, eventsCautions);
                ShowtoGUI_EventCautions();
                ShowtoGUI_MajorAlarmProfile();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load events configurations", ex);
            }
        }

        private void LoadParams_Events(Param_MajorAlarmProfile majorAlarmProfile = null,
            List<Param_EventsCaution> eventsCautions = null)
        {
            ///ListEventCautions.Clear();
            List<Param_EventsCaution> _ListEventCautions = new List<Param_EventsCaution>();
            for (int i = 0; i < ListEventInfo.Count; i++)
            {
                Param_EventsCaution Param_EventsCaution_obj = new Param_EventsCaution();
                if (ListEventInfo[i].EventId != null)
                {
                    Param_EventsCaution_obj.EventId = ListEventInfo[i].EventId;
                    Param_EventsCaution_obj.Event_Name = ListEventInfo[i].EventName;
                    Param_EventsCaution_obj.Event_Code = ListEventInfo[i].EventCode;
                    Param_EventsCaution_obj.CautionNumber = (byte)ListEventInfo[i].EventCode;
                    _ListEventCautions.Add(Param_EventsCaution_obj);
                }

                ///Param_EventsCaution_obj.Event_Name = ListEventInfo[i].EventName;
                ///Param_EventsCaution_obj.Event_Code = ListEventInfo[i].EventCode;
                ///Param_EventsCaution_obj.CausionNumber = (byte)ListEventInfo[i].EventCode;
                _ListEventCautions.Sort(Param_EventsCaution.Param_EventsCautionSort_Helper);
                if (_ListEventCautions != ListEventCautions)
                    ListEventCautions = _ListEventCautions;
            }
            ///Init Major Alarm Object Using Configurations
            Param_MajorAlarmProfile_obj = new Param_MajorAlarmProfile(ListEventInfo, _ListEventCautions.Count);
            #region ///Populate Param_EventsCaution Loaded

            if (eventsCautions != null && eventsCautions.Count > 0)
            {
                eventsCautions.Sort(Param_EventsCaution.Param_EventsCautionSort_Helper);
                Param_EventsCaution evCautionToModify = null;
                foreach (Param_EventsCaution evCaution in eventsCautions)
                {
                    if (eventsCautions == null)///Skip For Null
                        continue;
                    evCautionToModify = ListEventCautions.Find((x) => x != null && x.EventId == evCaution.EventId);
                    if (evCautionToModify == null)///Skip For Null
                        continue;
                    ///Copy Loaded Param Details
                    evCautionToModify.CautionNumber = evCaution.CautionNumber;
                    evCautionToModify.FlashTime = evCaution.FlashTime;
                    evCautionToModify.Flag = evCaution.Flag;
                }
            }

            #endregion
            #region Populate Param_MajorAlarmProfile Loaded

            if (majorAlarmProfile != null && majorAlarmProfile.AlarmItems.Count > 0)
            {
                majorAlarmProfile.AlarmItems.Sort(Param_MajorAlarmProfile.MajorAlarmSort_Helper);
                MajorAlarm majorAlarmToModify = null;
                foreach (var majorAlarm in majorAlarmProfile.AlarmItems)
                {
                    if (majorAlarm == null)
                        continue;
                    try
                    {
                        majorAlarmToModify = Param_MajorAlarmProfile_obj.AlarmItems.Find((x) => x.Info.EventId == majorAlarm.Info.EventId);
                    }
                    catch
                    {
                        majorAlarmToModify = null;
                    }
                    ///Copy Loaded Param Details
                    if (majorAlarmToModify != null)
                    {
                        majorAlarmToModify.IsMajorAlarm = majorAlarm.IsMajorAlarm;
                        majorAlarmToModify.IsTriggered = majorAlarm.IsTriggered;
                    }
                }
            }

            #endregion
        }

        private void ClearGrid(DataGridView gridName)
        {
            try
            {
                ((System.ComponentModel.ISupportInitialize)(gridName)).BeginInit();
                gridName.Rows.Clear();

                //while (gridName.Rows.Count > 0)
                //{
                //    gridName.Rows.RemoveAt(0);
                //}

            }
            finally
            {
                ((System.ComponentModel.ISupportInitialize)(gridName)).EndInit();
            }
        }

        internal AccessRights GetMeterEventRight(MeterEvent MeterEventArg)
        {
            AccessRights Rights = null;
            try
            {
                if (MeterEventRights != null)
                    Rights = MeterEventRights.Find((x) => x != null && x.QuantityType == typeof(MeterEvent) && String.Equals(x.QuantityName, MeterEventArg.ToString()));
            }
            catch { }
            return Rights;
        }

        #endregion

        private void pnlEvents_Load(object sender, EventArgs e)
        {
            try
            {
                if (Application_Controller != null)
                {
                    ///Interface Init Work
                    //=========================================================================================================
                    //=========================================================================================================
                    Param_Controller = Application_Controller.Param_Controller;
                    Application_Process = Application_Controller.Applicationprocess_Controller.ApplicationProcess;
                    connectionManager = Application_Controller.ConnectionManager;
                    AP_Controller = Application_Controller.Applicationprocess_Controller;
                    LoadProfile_Controller = Application_Controller.LoadProfile_Controller;
                    ConnController = Application_Controller.ConnectionController;
                    Instantanous_Controller = Application_Controller.Billing_Controller;
                    Event_Controller = Application_Controller.EventController;

                    ListEventInfo = new List<EventInfo>();
                    ListEventLogInfo = new List<EventLogInfo>();
                    ListEventItem = new List<EventItem>();
                    listEventData = new List<EventData>();
                    ListEventCautions = new List<Param_EventsCaution>();

                    grid_Events_Counters.Visible = false;
                    grid_Events_Log.Visible = false;
                    Instantaneous_Class_obj = new Instantaneous_Class();
                    obj_CustomerCode = new Param_Customer_Code();

                    ///====================================================
                    ///====================================================
                    ///  ClearGrid(grid_Events)
                    tabControl1_SelectedIndexChanged(this, new EventArgs());
                    ShowtoGUI_Flag = true;

                    LoadAlarm(null, ConnController.SelectedMeter.MeterModel); //v3.3.7 QC
                    radio_Events_CountersOnly.Checked = true;
                    combo_EventFilters.SelectedIndex = 0;

                    initialize_EventMap();

                    bgw_securityData.DoWork += new DoWorkEventHandler(bgw_securityData_DoWork);
                    bgw_securityData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_securityData_RunWorkerCompleted);

                    bgw_ReadLogBook.DoWork += new DoWorkEventHandler(bgw_ReadLogBook_DoWork);
                    bgw_ReadLogBook.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_ReadLogBook_RunWorkerCompleted);

                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                ///throw new Exception("Error Initializing Event Interface Properly", ex);
                Notification Notifier = new Notification("Error Initializing",
                    String.Format("Error Initializing Event Interface Properly\r\n{0}", ex.Message));
            }
        }

        #region showMethods_Interface

        private void showEventCountersToGrid()
        {
            try
            {
                grid_Events_Log.Visible = false;
                grid_Events_Counters.Visible = true;
                combo_Events_SelectedITems.Visible = false;
                lbl_comboEvents.Visible = false;

                ClearGrid(grid_Events_Counters);
                if (ListEventItem.Count <= 0)
                {
                    Notification Notifier = new Notification("Not Available",
                    String.Format("Event Data not available"));
                }
                else
                {
                    foreach (EventItem item in ListEventItem)
                    {
                        grid_Events_Counters.Rows.Add();
                        grid_Events_Counters.Rows[grid_Events_Counters.Rows.Count - 1].HeaderCell.Value = grid_Events_Counters.Rows.Count.ToString();
                        grid_Events_Counters[0, grid_Events_Counters.Rows.Count - 1].Value = item.EventInfo.EventName;
                        grid_Events_Counters[1, grid_Events_Counters.Rows.Count - 1].Value = item.EventInfo.EventCode;
                        grid_Events_Counters[2, grid_Events_Counters.Rows.Count - 1].Value = item.EventCounter;
                    }
                }
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error",
                    String.Format("Error displaying event counters\r\n{0}", ex.Message));
            }
        }

        //============================================================================================
        private DateTime CaptureDateTime_Compensate(int EventCode, DateTime EventDateTimeStamp)
        {
            DateTime captureTime = new DateTime();
            switch (EventCode)
            {
                //case 214:		//Contactor Status Off
                //    //captureTime = item.EventDateTimeStamp-ParamConfigurationSet.
                //    break;
                //case 213:		//Contactor Status On
                //    break;
                case 121:		//CT Failure
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.CTFail;
                    break;
                case 123:		//High Neutral Current
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.HighNeutralCurrent;
                    break;
                case 120:		//Imbalance Voltage
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.ImbalanceVolt;
                    break;
                case 119:		//Over Current
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.OverCurrent;
                    break;
                case 218:		//Over Load End
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.OverLoad;
                    break;
                case 201:		//Over Load Start
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.OverLoad;
                    break;
                case 208:		//Over Volt End
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.OverVolt;
                    break;
                case 114:		//Over Voltage
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.OverVolt;
                    break;
                case 221:		//Phase Fail End
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.PhaseFail;
                    break;
                case 113:		//Phase Failure
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.PhaseFail;
                    break;
                case 124:		//Phase Sequence
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.PhaseSequence;
                    break;
                //case 112:		//Power Fail End
                //    captureTime = item.EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.CTFail;
                //    break;
                //case 111:		//Power Failure Start
                //    captureTime = item.EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.CTFail;
                //    break;
                case 122:		//PT Failure
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.PTFail;
                    break;
                case 216:		//Reverse Energy End
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.ReverseEnergy;
                    break;
                case 117:		//Reverse Energy Start
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.ReverseEnergy;
                    break;
                case 118:		//Reverse Polarity
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.ReversePolarity;
                    break;
                case 217:		//Tamper Energy End
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.ReversePolarity;
                    break;
                case 128:		//Tamper Energy Start
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.TamperEnergy;
                    break;
                case 205:		//Under Volt End
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.UnderVolt;
                    break;
                case 115:		//Under Voltage Start
                    captureTime = EventDateTimeStamp - ParamConfigurationSet.ParamMonitoringTime.UnderVolt;
                    break;
                default:
                    captureTime = EventDateTimeStamp;
                    break;
            }
            return captureTime;
        }
        //============================================================================================
        private void showEventLogToGrid(EventData EventData_obj)
        {
            try
            {
                ClearGrid(grid_Events_Log);
                int dataset_row_counter = 1;

                //Remove Event Details Column
                EventData_obj.EventRecords.Sort((x, y) => y.EventCounter.CompareTo(x.EventCounter));
                //EventData savedEventsData = dbController.ReadSavedEventData(
                //    EventData_obj.EventInfo.EventCode,
                //    Application_Controller.ConnectionManager.ConnectionInfo.MSN, 
                //    EventData_obj.EventRecords[EventData_obj.EventRecords.Count - 1].EventCounter, 
                //    EventData_obj.EventRecords[0].EventCounter);

                //commented in v4.8.18
                //grid_Events_Log.Columns[r_EventDetails].Visible = false;

                int myTemp = 0;
                //foreach (var item in EventData_obj.EventRecords)
                //{

                //v4.8.23
                if (EventCodeList_Start.Contains(EventData_obj.EventRecords[0].EventInfo.EventCode) && IsDisplayEventDetail)
                {
                    grid_Events_Log.Columns["EventDetails"].HeaderText = "Event Details";
                    IsEvenIn_EventCodeList_Start = true;
                }
                else
                {
                    grid_Events_Log.Columns["EventDetails"].HeaderText = "";
                    IsEvenIn_EventCodeList_Start = false;
                }

                for (int index = 0; index < EventData_obj.EventRecords.Count; index++)
                {
                    EventItem item = EventData_obj.EventRecords[index];
                    grid_Events_Log.Rows.Add();
                    grid_Events_Log.Rows[grid_Events_Log.Rows.Count - 1].HeaderCell.Value = grid_Events_Log.Rows.Count.ToString();

                    if (true)
                    {
                        if (item.DateTimeStamp.IsDateTimeConvertible || item.DateTimeStamp.IsDateConvertible)
                            grid_Events_Log[0, grid_Events_Log.Rows.Count - 1].Value = item.EventDateTimeStamp.ToString(EventDateTimeFormat);
                        else grid_Events_Log[0, grid_Events_Log.Rows.Count - 1].Value = "--- " + item.EventDateTimeStamp.ToString();
                    }
                    //else //Compensation with Monitoring Time
                    //{
                    //    DateTime eventTime = CaptureDateTime_Compensate(item.EventInfo.EventCode, item.EventDateTimeStamp);
                    //    EventItem savedItem = savedEventsData.EventRecords.Find(x => x.EventCounter == item.EventCounter);
                    //    if (savedItem != null) eventTime = savedItem.EventDateTimeStamp;
                    //        EventData_obj.EventRecords[index].EventDateTimeStamp = eventTime;

                    //    grid_Events_Log[0, grid_Events_Log.Rows.Count - 1].Value = eventTime;
                    //}
                    grid_Events_Log[1, grid_Events_Log.Rows.Count - 1].Value = item.EventInfo.EventName;
                    grid_Events_Log[2, grid_Events_Log.Rows.Count - 1].Value = item.EventInfo.EventCode;
                    grid_Events_Log[3, grid_Events_Log.Rows.Count - 1].Value = item.EventCounter;
                    grid_Events_Log[4, grid_Events_Log.Rows.Count - 1].Value = String.IsNullOrEmpty(item.EventDetailStr) ? "Not Available" : item.EventDetailStr;

                    //v4.8.18
                    //4.8.23 //"Not available hidden"
                    //temp comment
                    if (IsEvenIn_EventCodeList_Start && !String.IsNullOrEmpty(item.EventDetailStr))
                        grid_Events_Log[4, grid_Events_Log.Rows.Count - 1].Value = Decode_Event_Detail(item.EventDetailStr);
                    else
                    {
                        grid_Events_Log[4, grid_Events_Log.Rows.Count - 1].Value = "";
                    }

                    int itemCode = item.EventInfo.EventCode;
                    /// if (itemCode.Equals(121) || itemCode.Equals(113) || itemCode.Equals(117) || itemCode.Equals(118) || itemCode.Equals(115) || itemCode.Equals(114) || itemCode.Equals(201) || itemCode.Equals(109) || itemCode.Equals(208))
                    if (isRecovery(itemCode) && EventData_obj.EventInfo.EventCode != 0)
                    {
                        Rpt_to_display = 1;
                        EventTitle = combo_Events_SelectedITems.SelectedItem.ToString();
                        if (EventTitle.Contains("Start"))
                        {
                            EventTitle = EventTitle.Remove(EventTitle.Length - 5, 5); //Removes "Start"
                        }

                        EventMap endCode = list_eventMap.Find(x => x.eventStartCode == itemCode);
                        if (endCode == null)
                            return;
                        EventData EndItem = tempEventLogs.Find(x => x.EventInfo.EventCode == endCode.eventEndCode);

                        dataset_row_counter++;

                    }
                    else //for all other Events
                    {
                        Rpt_to_display = 0;
                        EventTitle = combo_Events_SelectedITems.SelectedItem.ToString();
                        if (EventTitle.Contains("Start"))
                        {
                            EventTitle.Remove(EventTitle.Length - 5, 5);
                        }
                        dataset_row_counter++;
                    }


                }
                if (EventData_obj.EventRecords.Count <= 0)
                {
                    Notification Notifier = new Notification("Not Available",
                    String.Format("Event Data not available"));
                }

            }

            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error",
                    String.Format("Error displaying Event Logs\r\n{0}", ex.Message));
            }


        }

        private void showLogBook(EventData EventData_obj)
        {
            try
            {
                ClearGrid(grid_LogBook);
                if (EventData_obj.EventRecords.Count <= 0)
                {
                    Notification Notifier = new Notification("Not Available",
                    String.Format("Event Data not available"));
                    return;
                }

                AccessRights AccessRight = null;

                //Added By Azeem Inayat
                //Sort Event List// From Z to A
                EventData_obj.EventRecords.Sort((x, y) => y.EventCounter.CompareTo(x.EventCounter));
                EventData savedEventsData = null;
                if (IsEventTimeCompensationRequired)
                {
                    savedEventsData = dbController.readSavedLogBook(Application_Controller.ConnectionManager.ConnectionInfo.MSN, EventData_obj.EventRecords[EventData_obj.EventRecords.Count - 1].EventCounter, EventData_obj.EventRecords[0].EventCounter);

                }
                for (int index = 0; index < EventData_obj.EventRecords.Count; index++)
                {
                    EventItem item = EventData_obj.EventRecords[index];
                    if (item.EventInfo != null && item.EventInfo.EventId != null)
                        AccessRight = GetMeterEventRight((MeterEvent)item.EventInfo.EventId);

                    if (AccessRight != null && AccessRight.Read)
                    {
                        grid_LogBook.Rows.Add();
                        grid_LogBook.Rows[grid_LogBook.Rows.Count - 1].HeaderCell.Value = grid_LogBook.Rows.Count.ToString();

                        if (IsEventTimeCompensationRequired) //Event Capture Time compensation with Monitoring Time
                        {
                            DateTime eventTime = CaptureDateTime_Compensate(item.EventInfo.EventCode, item.EventDateTimeStamp);
                            EventItem savedItem = savedEventsData.EventRecords.Find(x => x.EventCounter == item.EventCounter);
                            if (savedItem != null) eventTime = savedItem.EventDateTimeStamp;

                            EventData_obj.EventRecords[index].EventDateTimeStamp = eventTime;

                            grid_LogBook[0, grid_LogBook.Rows.Count - 1].Value = eventTime.ToString(EventDateTimeFormat);
                        }
                        else
                        {
                            if (item.DateTimeStamp.IsDateTimeConvertible || item.DateTimeStamp.IsDateConvertible)
                                grid_LogBook[0, grid_LogBook.Rows.Count - 1].Value = item.EventDateTimeStamp.ToString(EventDateTimeFormat);//String.Format("{0:dd MMM-yyyy HH:mm:ss}", item.EventDateTimeStamp);
                            else grid_LogBook[0, grid_LogBook.Rows.Count - 1].Value = "--- " + item.EventDateTimeStamp.ToString();

                        }


                        grid_LogBook[1, grid_LogBook.Rows.Count - 1].Value = item.EventInfo.EventName;
                        grid_LogBook[2, grid_LogBook.Rows.Count - 1].Value = item.EventInfo.EventCode;
                        grid_LogBook[3, grid_LogBook.Rows.Count - 1].Value = item.EventCounter;
                        //grid_LogBook[4, grid_LogBook.Rows.Count - 1].Value = String.IsNullOrEmpty(item.EventDetailStr) ? "Not Available" : item.EventDetailStr; 
                    }
                }
                if (grid_LogBook.Rows.Count <= 0)
                {
                    Notification Notifier = new Notification("Not Available",
                    String.Format("Event Data not available"));
                }
            }

            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error",
                    String.Format("Error displaying Event Logs\r\n{0}", ex.Message));
            }
        }

        private void ShowtoGUI_EventCautions()
        {
            AccessRights AccessRight = null;
            try
            {
                if (ShowtoGUI_Flag)
                {
                    ClearGrid(grid_Events);
                    for (int i = 0; i < ListEventCautions.Count; i++)
                    {
                        //if (ListEventCautions[i].EventId == MeterEvent.TimeBaseEvent_1 || ListEventCautions[i].EventId == MeterEvent.TimeBaseEvent_2)
                        //{
                        //    grid_Events.Rows.Add();
                        //    grid_Events[r_EventName, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].Event_Name;
                        //    grid_Events[r_EventCode, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].Event_Code;
                        //    grid_Events[r_Caution, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].CausionNumber;
                        //    grid_Events[r_DisplayCaution, grid_Events.Rows.Count - 1].Value = false;
                        //    grid_Events[r_FlashTime, grid_Events.Rows.Count - 1].Value = "x";
                        //    grid_Events[r_IsMajorAlarm, grid_Events.Rows.Count - 1].Value = true;

                        //    ///Mark As ReadOnly Column
                        //    grid_Events[r_Caution, grid_Events.Rows.Count - 1].ReadOnly = true;
                        //    grid_Events[r_DisplayCaution, grid_Events.Rows.Count - 1].ReadOnly = true;
                        //    grid_Events[r_FlashTime, grid_Events.Rows.Count - 1].ReadOnly = true;
                        //    //  grid_Events[r_IsMajorAlarm, grid_Events.Rows.Count - 1].ReadOnly = true;

                        //}
                        // else 
                        if (ListEventCautions[i].EventId != null)
                        {
                            //Skip EventItem If No Read/Write Access For Event Item
                            AccessRight = GetMeterEventRight((MeterEvent)ListEventCautions[i].EventId);
                            //if (AccessRight != null && !(AccessRight.Read || AccessRight.Write))
                            //    continue;
                            if (AccessRight == null || !(AccessRight.Read))
                                continue;

                            grid_Events.Rows.Add();

                            grid_Events[r_EventName, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].Event_Name;
                            grid_Events[r_EventCode, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].Event_Code;

                            try
                            {
                                grid_Events[r_Caution, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].CautionNumber;
                                grid_Events[r_FlashTime, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].FlashTime;
                                // grid_Events[r_ReadCaution, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].to_Read;
                                ///Display Caution
                                grid_Events[r_DisplayCaution, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].IsDisplayCaution;
                                ///display flash 
                                grid_Events[r_IsFlash, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].IsFlashCaution;
                                ///Read Caution
                                grid_Events[r_ReadCaution, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].IsReadCaution;
                                ///Event Caution Flags
                                grid_Events[r_IsEnable, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].IsDisableLog;
                                grid_Events[r_IsLogBookEvent, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].IsExcludeFromLogBook;
                            }
                            catch
                            { }
                        }
                    }
                    grid_Events.Sort(Event_Name, ListSortDirection.Ascending);
                    Event_Name.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    // grid_Events.Sort(grid_Events.Columns[r_EventName], ListSortDirection.Ascending);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ShowtoGUI_MajorAlarmProfile()
        {
            ShowtoGUI_Flag = false;
            for (int i = 0; i < grid_Events.Rows.Count - 1; i++)
            {
                grid_Events[r_IsMajorAlarm, i].Value = false;
            }

            //timebase events are Major Alarms by default
            //for (int i = 0; i < grid_Events.Rows.Count - 1; i++)
            //{
            //    if (Convert.ToInt32(grid_Events[r_EventCode, i].Value) == 211 || Convert.ToInt32(grid_Events[r_EventCode, i].Value) == 212)
            //    {
            //        grid_Events[r_IsMajorAlarm, i].Value = true;
            //    }
            //    Application.DoEvents();
            //}

            try
            {
                foreach (DataGridViewRow item in grid_Events.Rows)
                {
                    string eventName = Convert.ToString(item.Cells[r_EventName].Value);
                    if (item.Cells[r_AlarmStatus] != null)
                        item.Cells[r_AlarmStatus].Style.BackColor = item.Cells[r_EventName].Style.BackColor;
                    MajorAlarm Alarm = Param_MajorAlarmProfile_obj.AlarmItems.Find((x) => x.Info != null && x.Info.EventName.Equals(eventName));
                    if (Alarm != null)
                    {
                        //if (Alarm.Info.EventCode != 211 && Alarm.Info.EventCode != 212)
                        //{
                        //    item.Cells[r_IsMajorAlarm].Value = Alarm.IsMajorAlarm;
                        //}
                        try
                        {
                            item.Cells[r_IsMajorAlarm].Value = Alarm.IsMajorAlarm;
                            item.Cells[r_AlarmStatus].Value = (Alarm.IsTriggered) ? "TRG" : "RST";
                            item.Cells[r_AlarmStatus].Style.BackColor = (Alarm.IsTriggered) ?
                                Color.Red : item.Cells[r_EventName].Style.BackColor;

                            if (Alarm.IsTriggered)
                            {
                                item.Cells[r_ResetAlarmStatus].Value = Alarm.IsReset;
                                item.Cells[r_ResetAlarmStatus].ReadOnly = false;
                            }
                            else
                            {
                                item.Cells[r_ResetAlarmStatus].Value = false;
                                item.Cells[r_ResetAlarmStatus].ReadOnly = true;
                            }
                        }
                        catch
                        {
                        }
                    }
                    //for (int k = 0; k < ListEventCautions.Count; k++)
                    //{
                    //    if (ListEventCautions[k].Event_Code == item)
                    //    {
                    //        grid_Events[r_IsMajorAlarm, k].Value = true;
                    //        break;
                    //    }
                    //}
                }
                ShowtoGUI_Flag = true;
            }
            catch (Exception)
            {
                ShowtoGUI_Flag = true;
                return;
            }
            finally
            {
                ShowtoGUI_Flag = true;
            }
        }

        #endregion

        private void AddEventItemsToList()
        {
            AccessRights AccessRight = null;
            if (this.IsHandleCreated || true)
            {
                list_Event_SelectableEvents.Items.Clear();
                foreach (EventInfo Eventitem in ListEventLogInfo)
                {
                    if (Eventitem.EventCode == 0) continue;
                    if (Eventitem.EventId != null)
                        AccessRight = GetMeterEventRight((MeterEvent)Eventitem.EventId);
                    if (AccessRight != null && (AccessRight.Read || AccessRight.Write))
                        list_Event_SelectableEvents.Items.Add(Eventitem);
                    else if (AccessRight == null)
                        list_Event_SelectableEvents.Items.Add(Eventitem);
                }

                //Added by Azeem
                tabControl1_SelectedIndexChanged(this, new EventArgs());
            }
        }

        private void Save_MajorAlarmProfile()
        {
            try
            {
                foreach (DataGridViewRow item in grid_Events.Rows)
                {
                    string eventName = Convert.ToString(item.Cells[r_EventName].Value);
                    MajorAlarm Alarm = Param_MajorAlarmProfile_obj.AlarmItems.Find((x) => x.Info != null && x.Info.EventName.Equals(eventName));
                    Param_MajorAlarmProfile_obj.AlarmItems.Remove(Alarm);
                    if (Alarm != null)
                    {
                        Alarm.IsMajorAlarm = Convert.ToBoolean(item.Cells[r_IsMajorAlarm].Value);
                        //if (Alarm.Info.EventId == MeterEvent.TimeBaseEvent_1 ||
                        //   Alarm.Info.EventId == MeterEvent.TimeBaseEvent_2)
                        //{
                        //    Alarm.IsMajorAlarm = true;
                        //}
                        Alarm.IsReset = Convert.ToBoolean(item.Cells[r_ResetAlarmStatus].Value);
                        Param_MajorAlarmProfile_obj.AlarmItems.Add(Alarm);
                    }
                    //for (int k = 0; k < ListEventCautions.Count; k++)
                    //{
                    //    if (ListEventCautions[k].Event_Code == item)
                    //    {
                    //        grid_Events[r_IsMajorAlarm, k].Value = true;
                    //        break;
                    //    }
                    //}
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public void Save_Events_Data()
        {
            setEventsDataForDB();
            if (!MyDataBase.SaveEventsToDataBase(List_Records, "L"))
            {
                ///MessageBox.Show("Error saving to Database");
                Notification Notifier = new Notification("Error Saving", "Error saving to Database");
            }
            else
            {
                //MessageBox.Show("Events Values Added to Database!!!!");
                //Notification Notifier = new Notification("Successful Saving", "Events Values Added to Database"); //v4.8.30
            }
            List_Records.Clear();
        }

        private void grid_Events_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string column = "";
            int row = 0;
            int EventCode = 0;
            int flashTime = 0;
            byte newValue = 0;
            try
            {
                if (ShowtoGUI_Flag)
                {
                    if (true && grid_Events.CurrentCell != null)//if (eventListInitialized)
                    {
                        column = grid_Events.CurrentCell.OwningColumn.Name;
                        row = grid_Events.CurrentCell.RowIndex;

                        //************************************************
                        //verify that entered value is numeric
                        //************************************************
                        //Commented in v4.8.32 for test by azeem

                        try
                        {
                            //newValue = Convert.ToByte(grid_Events[column, row].Value);//v4.8.32 commented

                            //Try Parse Grid Value
                            //v4.8.30 commented
                            var _val = grid_Events[column, row].Value.ToString();
                            if (!Byte.TryParse(_val, out newValue))
                            {
                                //return; //v4.8.32 commented
                            }
                        }
                        catch (Exception ex)
                        {
                            //grid_Events[column, row].Value = 0;
                            return;
                        }
                        //************************************************
                        //************************************************

                        if (!r_EventName.Equals(column) && !r_EventCode.Equals(column) && !r_IsMajorAlarm.Equals(column) &&
                            !r_DisplayCaution.Equals(column)) //Events Name and Major Alarm checkbox bypassed
                        {
                            newValue = Convert.ToByte(grid_Events[column, row].Value);
                        }
                        if (r_IsMajorAlarm.Equals(column)) //IS Major Alarm
                        {
                            Save_MajorAlarmProfile();
                        }
                        else if (r_ResetAlarmStatus.Equals(column)) //IS Alarm Reset Clicked
                        {
                            Save_MajorAlarmProfile();
                        }
                        grid_Events.CurrentCell.ErrorText = "";
                        if (r_Caution.Equals(column)) //caution number
                        {
                            if (newValue < 0 || newValue > 199)
                            {
                                //newValue = 1;
                                grid_Events.CurrentCell.ErrorText = "value must be between 1 and 199";
                            }
                            else
                            {
                                EventCode = Convert.ToInt32(grid_Events[1, row].Value);
                                for (int k = 0; k < ListEventCautions.Count; k++)
                                {
                                    if (ListEventCautions[k].Event_Code == EventCode)
                                    {
                                        ListEventCautions[k].CautionNumber = newValue;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                            if (r_FlashTime.Equals(column)) //flash time
                        {
                            if (newValue < 0 || newValue > 5)
                            {
                                newValue = 1;
                                grid_Events.CurrentCell.ErrorText = "value must be between 1 and 5";
                            }
                            else
                            {
                                EventCode = Convert.ToInt32(grid_Events[1, row].Value);
                                for (int k = 0; k < ListEventCautions.Count; k++)
                                {
                                    if (ListEventCautions[k].Event_Code == EventCode)
                                    {
                                        ListEventCautions[k].FlashTime = newValue;
                                        break;
                                    }
                                }
                            }
                        }
                        else if (r_ReadCaution.Equals(column))
                        {
                            bool isChecked = Convert.ToBoolean(grid_Events[column, row].Value);
                            EventCode = Convert.ToInt32(grid_Events[r_EventCode, row].Value);
                            for (int k = 0; k < ListEventCautions.Count; k++)
                            {
                                if (ListEventCautions[k].Event_Code == EventCode)
                                {
                                    ListEventCautions[k].IsReadCaution = isChecked;
                                    break;
                                }
                            }
                        }
                        else if (r_IsEnable.Equals(column)) ///IsEnableLog
                        {
                            bool isChecked = Convert.ToBoolean(grid_Events[column, row].Value);
                            EventCode = Convert.ToInt32(grid_Events[r_EventCode, row].Value);
                            for (int k = 0; k < ListEventCautions.Count; k++)
                            {
                                if (ListEventCautions[k].Event_Code == EventCode)
                                {
                                    ListEventCautions[k].IsDisableLog = isChecked;
                                    break;
                                }
                            }
                        }
                        else if (r_IsLogBookEvent.Equals(column)) ///IsIncludeLogBook
                        {
                            bool isChecked = Convert.ToBoolean(grid_Events[column, row].Value);
                            EventCode = Convert.ToInt32(grid_Events[r_EventCode, row].Value);
                            for (int k = 0; k < ListEventCautions.Count; k++)
                            {
                                if (ListEventCautions[k].Event_Code == EventCode)
                                {
                                    ListEventCautions[k].IsExcludeFromLogBook = isChecked;
                                    break;
                                }
                            }
                        }
                        else if (r_IsFlash.Equals(column)) //isFlash
                        {
                            bool isChecked = Convert.ToBoolean(grid_Events[column, row].Value);
                            EventCode = Convert.ToInt32(grid_Events[r_EventCode, row].Value);
                            for (int k = 0; k < ListEventCautions.Count; k++)
                            {
                                if (ListEventCautions[k].Event_Code == EventCode)
                                {
                                    ListEventCautions[k].IsFlashCaution = isChecked;
                                    break;
                                }
                            }
                        }
                        else if (r_DisplayCaution.Equals(column)) //Display Caution
                        {
                            bool isChecked = Convert.ToBoolean(grid_Events[column, row].Value);
                            EventCode = Convert.ToInt32(grid_Events[r_EventCode, row].Value);
                            for (int k = 0; k < ListEventCautions.Count; k++)
                            {
                                if (ListEventCautions[k].Event_Code == EventCode)
                                {
                                    ListEventCautions[k].IsDisplayCaution = isChecked;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void combo_Events_SelectedITems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grid_Events_Log.Visible = true;
                grid_Events_Counters.Visible = false;
                combo_Events_SelectedITems.Visible = true;
                lbl_comboEvents.Visible = true;
                if (combo_Events_SelectedITems.SelectedIndex > -1)
                {
                    if ((combo_Events_SelectedITems.SelectedItem) is EventInfo)
                    {
                        EventInfo TempEventInfo = (EventInfo)(combo_Events_SelectedITems.SelectedItem);

                        if (radio_Events_CountersOnly.Checked)
                        {
                            showEventCountersToGrid();
                        }
                        else
                        {

                            EventData TempEventData = this.listEventData.Find((x) => x.EventInfo.EventCode == TempEventInfo.EventCode);

                            if (TempEventData != null && TempEventData.EventRecords.Count > 0)
                            {
                                lbl_eventDetails.Text = "";
                                showEventLogToGrid(TempEventData);

                            }
                            else
                            {
                                ClearGrid(grid_Events_Log);
                                lbl_eventDetails.Text = "No LOG for selected Event";
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void Update_Event_Status_Label(String lbl_Status)
        {
            try
            {
                this.lbl_Status.Visible = true;
                this.lbl_Status.Text = lbl_Status;
            }
            catch (Exception ex) { this.lbl_Status.Visible = false; }
        }

        private long ReadLastNumberOfEvent()
        {
            try
            {
                //long eventCount = -1;
                if (combo_EventFilters.SelectedIndex == -1)
                    return -1;
                ///Read All Events
                ///Read Last 10 Events 
                ///Read Last 20 Events
                ///Read Last 30 Events
                ///Read Last 40 Events
                ///Read Last 50 Events
                ///Read Last 100 Events
                ///Read Last 150 Events
                ///Read Last 200 Events
                ///Read Last 250 Events
                String txt = combo_EventFilters.SelectedItem.ToString();
                string[] splits = txt.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                long retVal = long.MinValue;
                foreach (var strVal in splits)
                {
                    if (long.TryParse(strVal, out retVal))
                        break;
                    else
                        retVal = long.MinValue;
                }
                return retVal;
            }
            catch (Exception)
            {
                return long.MinValue;
            }
        }

        private void check_SelectAllEvents_CheckedChanged(object sender, EventArgs e)
        {
            if (!check_SelectAllEvents.Checked)
                for (int i = 0; i < list_Event_SelectableEvents.Items.Count; i++)
                {
                    list_Event_SelectableEvents.SetItemChecked(i, false);
                }
            else
                for (int i = 0; i < list_Event_SelectableEvents.Items.Count; i++)
                {
                    list_Event_SelectableEvents.SetItemChecked(i, true);
                }
        }

        private void radio_Events_CountersOnly_CheckedChanged(object sender, EventArgs e)
        {
            grid_Events_Log.Visible = false;
            grid_Events_Counters.Visible = true;

            combo_Events_SelectedITems.Visible = false;
            lbl_comboEvents.Visible = false;
        }

        private void radio_Events_CompleteLog_CheckedChanged(object sender, EventArgs e)
        {
            grid_Events_Log.Visible = true;
            grid_Events_Counters.Visible = false;

            combo_Events_SelectedITems.Visible = true;
            lbl_comboEvents.Visible = true;

            if (!radio_Events_CompleteLog.Checked)
            {
                check_E_addToDB.Checked = false;
                check_E_addToDB.Visible = false;
            }
            else
            {
                check_E_addToDB.Checked = false;
                //check_E_addToDB.Visible = true; //by Azeem Inayat
            }
        }

        private void txt_MonitoringTime_PowerFail_TBE2_Leave(object sender, EventArgs e)
        {
            try
            {
                // byte sec = (byte)txt_MonitoringTime_PowerFail_TBE2.Value.Second;
                //  byte min = (byte)txt_MonitoringTime_PowerFail_TBE2.Value.Minute;
                //  byte hour = (byte)txt_MonitoringTime_PowerFail_TBE2.Value.Hour;
                //  TBE2.Interval = Convert.ToUInt16(hour * 60 * 60 + min * 60 + sec);
                //     TBE1.Interval = Convert.ToUInt16(((ushort)min << 8) | (ushort)sec);
            }
            catch (Exception)
            {
                DateTime temp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                temp = temp.AddSeconds(60);
                //   txt_MonitoringTime_PowerFail_TBE2.Value = temp;
            }
        }

        private void txt_MonitoringTime_PowerFail_TBE1_Leave(object sender, EventArgs e)
        {
            try
            {
                // byte sec = (byte)txt_MonitoringTime_PowerFail_TBE1.Value.Second;
                // byte min = (byte)txt_MonitoringTime_PowerFail_TBE1.Value.Minute;
                //// byte hour = (byte)txt_MonitoringTime_PowerFail_TBE1.Value.Hour;
                // //TBE1.Interval = Convert.ToUInt16(hour * 60 * 60 + min * 60 + sec);
                // TBE1.Interval = Convert.ToUInt16(((ushort)min <<8)|(ushort)sec);
            }
            catch (Exception)
            {
                DateTime temp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                temp = temp.AddSeconds(60);
                //txt_MonitoringTime_PowerFail_TBE1.Value = temp;
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.SuspendLayout();
                this.tb_Events.SuspendLayout();
                this.fLP_MainButtons.SuspendLayout();
                this.pnl_Event_Container.SuspendLayout();

                //Added by Azeem
                if (tb_Events.TabPages.Count == 1) tb_Events.SelectedIndex = 0;

                btn_Events_GET.Visible = false;
                //btn_GET_EventsCautions.Visible = false;
                //btn_GetMajorAlarms.Visible = false;
                //btn_SET_EventsCautions.Visible = false;
                //btn_SetMajorAlarm.Visible = false;
                //btn_SetMajorStatus.Visible = false;
                flp_MajorAlarmButtons.Visible = false;

                radio_Events_CountersOnly.Checked = false;
                radio_Events_CompleteLog.Checked = false;

                //Combine Event Log
                if (tb_Events.SelectedTab == tab_LogBook)
                {
                    btn_Events_GET.Visible = true;
                }
                //Meter_Events Counters
                else if (tb_Events.SelectedTab == Meter_Events)
                {
                    btn_Events_GET.Visible = true;
                    ///Individual_Events.Controls.Remove(pnl_Event_Container);
                    if (!Meter_Events.Controls.Contains(pnl_Event_Container))
                    {
                        Meter_Events.Controls.Add(pnl_Event_Container);
                        pnl_Event_Container.Visible = true;
                    }
                    radio_Events_CountersOnly.Checked = true;
                }
                //Meter Individual Events
                else if (tb_Events.SelectedTab == Individual_Events)
                {
                    btn_Events_GET.Visible = true;
                    ///Meter_Events.Controls.Remove(pnl_Event_Container);
                    if (!Individual_Events.Controls.Contains(pnl_Event_Container))
                    {
                        Individual_Events.Controls.Add(pnl_Event_Container);
                        pnl_Event_Container.Visible = true;
                    }
                    radio_Events_CompleteLog.Checked = true;
                }
                //Program Event Cautions
                else if (tb_Events.SelectedTab == ProgramEvents)
                {
                    //btn_GET_EventsCautions.Visible = true;
                    //btn_GetMajorAlarms.Visible = true;
                    flp_MajorAlarmButtons.Visible = true;

                    //if (Application_Controller.CurrentUser.userTypeID == UserTypeID.Admin ||
                    //    Application_Controller.CurrentUser.userTypeID == UserTypeID.SuperAdmin)
                    //{
                    //    btn_SET_EventsCautions.Visible = true;
                    //    btn_SetMajorAlarm.Visible = true;
                    //    btn_SetMajorStatus.Visible = true;
                    //}
                }

            }
            finally
            {
                this.tb_Events.ResumeLayout();
                this.pnl_Event_Container.ResumeLayout();
                this.fLP_MainButtons.ResumeLayout();
                this.ResumeLayout();
            }
        }

        public bool DB_Add_DataSet_toWrite(DateTime arrivalTime, UInt64 eventQtyID,
            uint event_counter, string detail, bool isIndvidual, bool isNumber)
        {
            try
            {
                DBConnect.Insert_Evnent_CL_Counter record = new DBConnect.Insert_Evnent_CL_Counter();
                record.arrival_time = arrivalTime;
                record.msn = connectionManager.ConnectionInfo.MSN; ;
                record.qty_id = eventQtyID;
                record.counter = event_counter;
                record.description = detail;
                record.is_Indvidual = isIndvidual;
                List_Records.Add(record);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void setEventsDataForDB()
        {
            int count = combo_Events_SelectedITems.Items.Count;
            EventLogInfo eventLogInfo = null;

            for (int i = 0; i < count; i++)
            {
                if ((combo_Events_SelectedITems.Items[i]) is EventInfo)
                {
                    EventInfo TempEventInfo = (EventInfo)(combo_Events_SelectedITems.Items[i]);
                    EventData TempEventData = this.listEventData.Find((x) => x.EventInfo.EventCode == TempEventInfo.EventCode);
                    foreach (var item in TempEventData.EventRecords)
                    {
                        if (item.EventInfo is EventLogInfo)
                        {
                            eventLogInfo = (EventLogInfo)item.EventInfo;
                            StOBISCode temp = eventLogInfo.EventLogIndex;
                            if (TempEventInfo.EventCode == 0)
                                DB_Add_DataSet_toWrite((DateTime)item.DateTimeStamp.GetDateTime(), temp.OBIS_Value, item.EventCounter,
                                    item.EventDetailStr, false, true);
                            else
                                DB_Add_DataSet_toWrite((DateTime)item.DateTimeStamp.GetDateTime(), temp.OBIS_Value, item.EventCounter,
                                    item.EventDetailStr, true, true);
                        }
                    }
                }
            }
        }

        public void UpdateReadWriteStatus(bool IsReadWriteBusy)
        {
            try
            {
                ///Disable IO Read Write btns 
                if (IsReadWriteBusy)
                {
                    List<object> Controls = new List<object>();
                    foreach (var item in this.Controls)
                    {
                        Controls.Add(item);
                    }

                    while (Controls.Count > 0)
                    {
                        Control control = (Control)Controls[0];
                        Controls.Remove(control);
                        foreach (var item in control.Controls)
                        {
                            Controls.Add(item);
                        }
                        if (control is KryptonButton)
                        {
                            ((KryptonButton)control).Enabled = false;
                        }
                        else if (control is Button)
                        {
                            ((Button)control).Enabled = false;
                        }
                        else if (control is CheckedListBox)
                        {
                            ((CheckedListBox)control).Enabled = false;
                        }
                        else if (control is ComboBox)
                        {
                            ((ComboBox)control).Enabled = false;
                        }
                        else if (control is RadioButton)
                        {
                            ((RadioButton)control).Enabled = false;
                        }
                        else if (control is CheckBox)
                        {
                            ((CheckBox)control).Enabled = false;
                        }
                    }

                    btn_Events_GET.Enabled = false;
                    btn_SetMajorAlarm.Enabled = false;
                    btn_GetMajorAlarms.Enabled = false;
                    btn_SET_EventsCautions.Enabled = false;
                    btn_GET_EventsCautions.Enabled = false;
                    btn_SetMajorStatus.Enabled = false;
                    btn_ReadSecurityData.Enabled = false;

                    combo_EventFilters.Enabled = false;
                    radio_Events_CountersOnly.Enabled = false;
                    gp_EventData.Enabled = false;
                    radio_Events_CompleteLog.Enabled = false;
                    list_Event_SelectableEvents.Enabled = false;
                }
                ///Enable Read Write btns
                else
                {

                    List<object> Controls = new List<object>();
                    foreach (var item in this.Controls)
                    {
                        Controls.Add(item);
                    }
                    while (Controls.Count > 0)
                    {
                        Control control = (Control)Controls[0];
                        Controls.Remove(control);
                        foreach (var item in control.Controls)
                        {
                            Controls.Add(item);
                        }
                        if (control is KryptonButton)
                        {
                            ((KryptonButton)control).Enabled = true;
                        }
                        else if (control is Button)
                        {
                            ((Button)control).Enabled = true;
                        }
                        else if (control is CheckedListBox)
                        {
                            ((CheckedListBox)control).Enabled = true;
                        }
                        else if (control is ComboBox)
                        {
                            ((ComboBox)control).Enabled = true;
                        }
                        else if (control is RadioButton)
                        {
                            ((RadioButton)control).Enabled = true;
                        }
                        else if (control is CheckBox)
                        {
                            ((CheckBox)control).Enabled = true;
                        }
                        btn_Events_GET.Enabled = true;
                        btn_SetMajorAlarm.Enabled = true;
                        btn_GetMajorAlarms.Enabled = true;
                        btn_SET_EventsCautions.Enabled = true;
                        btn_GET_EventsCautions.Enabled = true;
                        btn_SetMajorStatus.Enabled = true;
                        btn_ReadSecurityData.Enabled = true;

                        combo_EventFilters.Enabled = true;
                        radio_Events_CountersOnly.Enabled = true;
                        gp_EventData.Enabled = true;
                        radio_Events_CompleteLog.Enabled = true;
                        list_Event_SelectableEvents.Enabled = true;
                        //progressBar1.Visible = false;
                        //progressBar1.Enabled = false;
                        //AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(false);
                        lbl_Status.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                {
                    List<object> Controls = new List<object>();
                    foreach (var item in this.Controls)
                    {
                        Controls.Add(item);
                    }
                    while (Controls.Count > 0)
                    {
                        Control control = (Control)Controls[0];
                        Controls.Remove(control);
                        foreach (var item in control.Controls)
                        {
                            Controls.Add(item);
                        }
                        if (control is KryptonButton)
                        {
                            ((KryptonButton)control).Enabled = true;
                        }
                        else if (control is Button)
                        {
                            ((Button)control).Enabled = true;
                        }
                        else if (control is CheckedListBox)
                        {
                            ((CheckedListBox)control).Enabled = true;
                        }
                        else if (control is ComboBox)
                        {
                            ((ComboBox)control).Enabled = true;
                        }
                        else if (control is RadioButton)
                        {
                            ((RadioButton)control).Enabled = true;
                        }
                        else if (control is CheckBox)
                        {
                            ((CheckBox)control).Enabled = true;
                        }
                    }
                    gp_EventData.Enabled = true;
                    //progressBar1.Visible = false;
                    //progressBar1.Enabled = false;
                    //AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(false);
                    lbl_Status.Visible = false;

                }

            }
        }

        internal void Reset_State()
        {
            try
            {
                ///ClearGrid(grid_Events);
                Dataset_Events.DataTable_Meter_Events.Clear(); //Added by Azeem
                ClearGrid(grid_Events_Log);
                ClearGrid(grid_LogBook);
                tabControl1_SelectedIndexChanged(this, new EventArgs());


            }
            catch
            {
            }
            finally
            {
                ApplyRights_IsEventCompensationRequired();
            }
        }

        #region NEW_REPORT_METHODS

        #region Decode_Event_Detail
        private string Decode_Event_Detail(string EventDetailStr)
        {
            #region Old_Decoder_from_Rashad
            //if (!String.IsNullOrEmpty(EventDetailStr)
            //          && (EventCode == 113     //Phase Fail Start
            //          || EventCode == 114      // Under Volt Start
            //          || EventCode == 115      //Over Volt Start
            //          || EventCode == 117      //Reverse Energy Start
            //          || EventCode == 201      //OverLoad Start
            //          || EventCode == 118      //Reverse Polarity Start
            //          ))
            //{
            //    byte b = (byte)(Convert.ToByte(EventDetailStr.Substring(1, 2), 16) & 7);
            //    return
            //          ((byte)(b & 1) == 1 ? "A " : "- ") +
            //          ((byte)(b & 2) == 2 ? "B " : "- ") +
            //          ((byte)(b & 4) == 4 ? "C " : "- ");
            //}
            //return "Not Available.";
            #endregion

            //4.8.23
            byte b = (byte)(Convert.ToByte(EventDetailStr.Substring(1, 2), 16) & 0xF); //Get lower Nibble
            if (b < 8) //A, B, C
            {
                return
                      ((byte)(b & 1) == 1 ? "A " : "- ") +    // 0001
                      ((byte)(b & 2) == 2 ? "B " : "- ") +    // 0010 
                      ((byte)(b & 4) == 4 ? "C " : "- ");     // 0100
            }
            else if (b >= 8 && b <= 0xF) //Total (Sum of A+B+C)           // 1000
            {
                return "A + B + C";
            }
            else if (b == 0xF) // Total and on all Phases     // 1111
            {
                return "A + B + C.";
            }
            return "Not Available.";
        }
        #endregion

        public void Find_Recovery_Event(ref bool _recovery_in_2, ref EventData evData_dest, int RecoveryEventCode, bool isStartEvent)
        {
            evData_dest = this.listEventData.Find((x) => x.EventInfo.EventCode == RecoveryEventCode);
            _recovery_in_2 = isStartEvent;
        }

        public int getMeterEventsReportData()
        {
            //v4.8.16
            int reportType = 0;
            // 3 : Report of Single Event (only start or only End)
            // 2 : Report of Start with End

            Dataset_Events.DataTable_Meter_Events.Clear();
            if (radio_Events_CountersOnly.Checked)
            {
                foreach (DataGridViewRow row in grid_Events_Counters.Rows)
                {
                    Dataset_Events.DataTable_Meter_Events.Rows.Add();

                    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_detail = "";
                    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_date = "";
                    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_name =
                        Convert.ToString(row.Cells["EventsName"].Value);
                    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_code =
                        Convert.ToString(row.Cells["EventsCode"].Value);
                    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_counter =
                        Convert.ToString(row.Cells["EventsCounter"].Value);
                }
            }
            else if (radio_Events_CompleteLog.Checked)
            {
                #region Events with Recovery

                if (combo_Events_SelectedITems.SelectedIndex > -1)
                {
                    if ((combo_Events_SelectedITems.SelectedItem) is EventInfo)
                    {
                        EventInfo TempEventInfo = (EventInfo)(combo_Events_SelectedITems.SelectedItem);

                        EventData TempEventData = this.listEventData.Find((x) => x.EventInfo.EventCode == TempEventInfo.EventCode);

                        bool IsRecoveryIn_2 = false;
                        EventData TempEventData2 = null;
                        EventData EventData_For_Swap = null; //v4.8.23

                        if (TempEventData != null && TempEventData.EventRecords.Count > 0)
                        {
                            //int eventCodeToMatch = -1;
                            switch (TempEventInfo.EventCode)
                            {
                                //========================================== 1
                                case 111:   //Power Fail
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 112, true);
                                    break;
                                case 112:   //Power Fail End
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 111, false);
                                    break;
                                //========================================== 2
                                case 113:   //Phase Fail
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 221, true);
                                    break;
                                case 221:   //Phase Fail End
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 113, false);
                                    break;
                                //========================================== 3
                                case 114:   //Over Volt
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 208, true);
                                    break;
                                case 208:   //Over Volt End
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 114, false);
                                    break;
                                //========================================== 4
                                case 201:   //Over Load
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 218, true);
                                    break;
                                case 218:   //Over Load End
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 201, false);
                                    break;
                                //========================================== 5
                                case 115:   //Under Volt
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 205, true);
                                    break;
                                case 205:   //Under Volt End
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 115, false);
                                    break;
                                //========================================== 6
                                case 117:   //Reverse Energy
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 216, true);
                                    break;
                                case 216:   //Reverse Energy End
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 117, false);
                                    break;
                                //========================================== 7
                                case 128:   //Tamper Energy
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 217, true);
                                    break;
                                case 217:   //Tamper Energy End
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 128, false);
                                    break;
                                //========================================== 8
                                case 118:   //Reverse Polarity
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 223, true);
                                    break;
                                case 223:   //Reverse Polarity End
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 118, false);
                                    break;
                                //========================================== 9
                                case 222:   //MagneticFeild
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 225, true);
                                    break;
                                case 225:   //MagneticFeild_End
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 222, false);
                                    break;
                                //========================================== 10
                                case 121:   //CTFail
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 226, true);
                                    break;
                                case 226:   //CTFail_End                  
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 121, false);
                                    break;
                                //========================================== 11
                                case 122:   //PTFail
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 227, true);
                                    break;
                                case 227:   //PTFail_End                  
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 122, false);
                                    break;
                                //========================================== 12
                                case 109:   //Software_Login
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 228, true);
                                    break;
                                case 228:   //Software_Logout       
                                    Find_Recovery_Event(ref IsRecoveryIn_2, ref TempEventData2, 109, false);
                                    break;
                                //========================================== 13

                                default:
                                    break;
                            }
                            int RecordDiff = -10;

                            //Start and End events will display in single report only when their count is equal or difference is exact 1
                            if (TempEventData2 != null && TempEventData2.EventRecords.Count > 0)
                                RecordDiff = TempEventData2.EventRecords.Count - TempEventData.EventRecords.Count;
                            /*Scenarios
                            *   0 = 5-5
                            *   1 = 5-4
                            *   2 = 5-3
                            *   -1 = 5-6
                            *   -2 = 5-7
                            */
                            if (RecordDiff < 2 && RecordDiff > -2)  //Verify that Selected event's both start and End Exists for each row
                            {
                                //v4.8.23 //Swapping to reduce code and optimization
                                if (!IsRecoveryIn_2)
                                {
                                    EventData_For_Swap = TempEventData;
                                    TempEventData = TempEventData2;
                                    TempEventData2 = EventData_For_Swap;
                                }
                                if (EventCodeList_Start.Contains(TempEventData.EventRecords[0].EventInfo.EventCode) && IsDisplayEventDetail)
                                {
                                    IsEvenIn_EventCodeList_Start = true;
                                }
                                else
                                {
                                    IsEvenIn_EventCodeList_Start = false;
                                }

                                foreach (EventItem itm in TempEventData.EventRecords)
                                {
                                    EventItem itm2 = TempEventData2.EventRecords.Find((x) => x.EventCounter == itm.EventCounter);

                                    Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Add();
                                    Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_name_Occurrence =
                                        (Convert.ToString(itm.EventInfo.EventName)).Replace("Start", "").Replace("start", "");
                                    Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_name_Recovery =
                                         (itm2 == null) ? "" : (Convert.ToString(itm.EventInfo.EventName));

                                    Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_code_Occurrence =
                                        Convert.ToString(itm.EventInfo.EventCode);
                                    Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_code_Recovery =
                                        (itm2 == null) ? "" : Convert.ToString(itm2.EventInfo.EventCode);

                                    Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_counter_Occurrence =
                                        Convert.ToString(itm.EventCounter);
                                    Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_counter_Recovery =
                                        (itm2 == null) ? "" : Convert.ToString(itm2.EventCounter);

                                    Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_date_Occurrence =
                                        (itm.EventDateTimeStamp).ToString(EventDateTimeFormat);
                                    Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_date_Recovery =
                                        (itm2 == null) ? "Continue" : (itm2.EventDateTimeStamp).ToString(EventDateTimeFormat);

                                    if (IsDisplayEventDetail && IsEvenIn_EventCodeList_Start)
                                    {
                                        Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_detail_Occurrence =
                                            String.IsNullOrEmpty(itm.EventDetailStr) ? "Not Available" : Decode_Event_Detail(itm.EventDetailStr);
                                        //Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_detail_Recovery =
                                        //    ((itm2 == null) || (String.IsNullOrEmpty(itm2.EventDetailStr)) ? "Not Available" : Decode_Event_Detail(itm2.EventInfo.EventCode, itm2.EventDetailStr));
                                    }
                                    else
                                    {
                                        Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_detail_Occurrence = "";
                                        Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_detail_Recovery = "";
                                    }
                                }
                                #region commented in v4.8.23 (by Azeem)
                                //if (IsRecoveryIn_2)
                                //{
                                //    foreach (EventItem itm in TempEventData.EventRecords)
                                //    {
                                //        EventItem itm2 = TempEventData2.EventRecords.Find((x) => x.EventCounter == itm.EventCounter);

                                //        Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Add();
                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_name_Occurrence =
                                //                (Convert.ToString(itm.EventInfo.EventName)).Replace("Start", "").Replace("start", "");
                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_name_Recovery =
                                //                 (itm2 == null)? "": (Convert.ToString(itm.EventInfo.EventName));

                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_code_Occurrence =
                                //                Convert.ToString(itm.EventInfo.EventCode);
                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_code_Recovery =
                                //                (itm2 == null) ? "" : Convert.ToString(itm2.EventInfo.EventCode);

                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_counter_Occurrence =
                                //                Convert.ToString(itm.EventCounter);
                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_counter_Recovery =
                                //                (itm2 == null) ? "" : Convert.ToString(itm2.EventCounter);

                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_date_Occurrence =
                                //                (itm.EventDateTimeStamp).ToString(EventDateTimeFormat);
                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_date_Recovery =
                                //                (itm2 == null) ? "Continue" : (itm2.EventDateTimeStamp).ToString(EventDateTimeFormat);

                                //            if (IsDisplayEventDetail)
                                //            {
                                //                Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_detail_Occurrence =
                                //                    String.IsNullOrEmpty(itm.EventDetailStr) ? "Not Available" : Decode_Event_Detail(itm.EventInfo.EventCode, itm.EventDetailStr);
                                //                Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_detail_Recovery =
                                //                    ((itm2 == null) || (String.IsNullOrEmpty(itm2.EventDetailStr)) ? "Not Available" : Decode_Event_Detail(itm2.EventInfo.EventCode, itm2.EventDetailStr));
                                //            }
                                //            else
                                //            {
                                //                Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_detail_Occurrence =
                                //                    String.IsNullOrEmpty(itm.EventDetailStr) ? "Not Available" : "Not Available.";
                                //                Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_detail_Recovery =
                                //                    ((itm2 == null) || (String.IsNullOrEmpty(itm2.EventDetailStr)) ? "Not Available" : "Not Available.");
                                //            }
                                //    }
                                //}
                                //else
                                //{
                                //    foreach (EventItem itm in TempEventData2.EventRecords)
                                //    {
                                //        EventItem itm2 = TempEventData.EventRecords.Find((x) => x.EventCounter == itm.EventCounter);

                                //        Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Add();
                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_name_Occurrence =
                                //                (Convert.ToString(itm.EventInfo.EventName)).Replace("Start", "").Replace("start", "");;
                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_name_Recovery =
                                //                (itm2 == null) ? "" : (Convert.ToString(itm2.EventInfo.EventName)) ;

                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_code_Occurrence =
                                //                Convert.ToString(itm.EventInfo.EventCode);
                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_code_Recovery =
                                //                (itm2 == null) ? "" : Convert.ToString(itm2.EventInfo.EventCode);

                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_counter_Occurrence =
                                //                Convert.ToString(itm.EventCounter);
                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_counter_Recovery =
                                //                (itm2 == null) ? "" : Convert.ToString(itm2.EventCounter);

                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_date_Occurrence =
                                //                (itm.EventDateTimeStamp).ToString(EventDateTimeFormat);
                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_date_Recovery =
                                //                (itm2 == null) ? "Continue" : (itm2.EventDateTimeStamp).ToString(EventDateTimeFormat);

                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_detail_Occurrence =
                                //                (String.IsNullOrEmpty(itm.EventDetailStr) ? "Not Available" : Decode_Event_Detail(itm.EventInfo.EventCode, itm.EventDetailStr)); 
                                //            Dataset_Events.DataTable_Meter_Events_With_Recovery[Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1].event_detail_Recovery =
                                //                ((itm2 == null) || String.IsNullOrEmpty(itm2.EventDetailStr) ? "Not Available" : Decode_Event_Detail(itm2.EventInfo.EventCode, itm2.EventDetailStr));
                                //    }
                                //}
                                #endregion
                                reportType = 2;
                            }
                            else
                            {
                                if (EventCodeList_Start.Contains(TempEventData.EventRecords[0].EventInfo.EventCode) && IsDisplayEventDetail)
                                    IsEvenIn_EventCodeList_Start = true;
                                else
                                    IsEvenIn_EventCodeList_Start = false;

                                foreach (EventItem itm in TempEventData.EventRecords)
                                {
                                    Dataset_Events.DataTable_Meter_Events.Rows.Add();
                                    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_name =
                                        (Convert.ToString(itm.EventInfo.EventName)).Replace("Start", "").Replace("start", "");

                                    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_code =
                                        Convert.ToString(itm.EventInfo.EventCode);

                                    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_counter =
                                        Convert.ToString(itm.EventCounter);

                                    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_date =
                                        (itm.EventDateTimeStamp).ToString(EventDateTimeFormat);

                                    if (IsEvenIn_EventCodeList_Start)
                                    {
                                        Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_detail =
                                            String.IsNullOrEmpty(itm.EventDetailStr) ? "Not Available" : Decode_Event_Detail(itm.EventDetailStr);
                                    }
                                    else
                                    {
                                        Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_detail = "";
                                    }
                                }



                                //foreach (DataGridViewRow row in grid_Events_Log.Rows)
                                //{
                                //    Dataset_Events.DataTable_Meter_Events.Rows.Add();
                                //    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_name =
                                //        Convert.ToString(row.Cells["EventName"].Value);
                                //    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_code =
                                //        Convert.ToString(row.Cells["EventCode"].Value);
                                //    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_counter =
                                //        Convert.ToString(row.Cells["EventCounter"].Value);
                                //    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_date =
                                //        Convert.ToString(row.Cells["Date_Time"].Value);
                                //    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_detail = "";
                                //        //(IsEvenIn_EventCodeList_Start)? Decode_Event_Detail( //Convert.ToString(row.Cells["EventDetails"].Value); //v4.8.23
                                //}
                                reportType = 3;
                            }


                        }
                    }
                }


                #endregion

                //foreach (DataGridViewRow row in grid_Events_Log.Rows)
                //{
                //    Dataset_Events.DataTable_Meter_Events.Rows.Add();
                //    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_name =
                //        Convert.ToString(row.Cells["EventName"].Value);
                //    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_code =
                //        Convert.ToString(row.Cells["EventCode"].Value);
                //    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_counter =
                //        Convert.ToString(row.Cells["EventCounter"].Value);
                //    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_date =
                //        Convert.ToString(row.Cells["Date_Time"].Value);
                //    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_detail =
                //        Convert.ToString(row.Cells["EventDetails"].Value);
                //}
            }
            return reportType;

        }

        public void getMajorAlarmReportData()
        {
            Dataset_Events.DataTable_Major_Alarms.Clear();
            foreach (DataGridViewRow row in grid_Events.Rows)
            {
                Dataset_Events.DataTable_Major_Alarms.Rows.Add();
                Dataset_Events.DataTable_Major_Alarms[row.Index].event_name = Convert.ToString(row.Cells["Event_Name"].Value);
                Dataset_Events.DataTable_Major_Alarms[row.Index].event_code = Convert.ToString(row.Cells["Event_Code"].Value);
                Dataset_Events.DataTable_Major_Alarms[row.Index].display_caution = Convert.ToString(row.Cells["Display_Caution"].Value);
                Dataset_Events.DataTable_Major_Alarms[row.Index].caution_number = Convert.ToString(row.Cells["Caution_Number"].Value);
                Dataset_Events.DataTable_Major_Alarms[row.Index].alarm_status = Convert.ToString(row.Cells["IsTriggered"].Value);

                Dataset_Events.DataTable_Major_Alarms[row.Index].is_flash = Convert.ToString(row.Cells["isFlash"].Value);
                Dataset_Events.DataTable_Major_Alarms[row.Index].flash_time = Convert.ToString(row.Cells["Flash_Time"].Value);
                Dataset_Events.DataTable_Major_Alarms[row.Index].read_caution = Convert.ToString(row.Cells["Read_Caution"].Value);
                Dataset_Events.DataTable_Major_Alarms[row.Index].is_major_alarm = Convert.ToString(row.Cells["IsMajorAlarm"].Value);
                Dataset_Events.DataTable_Major_Alarms[row.Index].reset_alarm_status = Convert.ToString(row.Cells["ResetAlarmStatus"].Value);
            }
        }

        #endregion

        #region Background_Workder

        private void bgw_SetMajorAlarms_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                grid_Events[0, 0].Selected = true;
                grid_Events.CurrentCell.Selected = false;
                Save_MajorAlarmProfile();
                if (Application_Process.Is_Association_Developed)
                {
                    Application_Controller.IsIOBusy = true;
                    //int tb1 = Param_MajorAlarmProfile_obj.EventCode.Find(x => x == 38);
                    //int tb2 = Param_MajorAlarmProfile_obj.EventCode.Find(x => x == 39);

                    //if (tb1 == 38 && tb2 == 39)
                    //{

                    Data_Access_Result temp = Param_Controller.SET_MajorAlarmProfile_Filter(Param_MajorAlarmProfile_obj);
                    //Data_Access_Result temp_1 = Param_Controller.SET_MajorAlarmProfile_Status(Param_MajorAlarmProfile_obj);
                    //MessageBox.Show(temp.ToString());
                    //    Notification Notifier = new Notification(String.Format("{0},Process Completed", 
                    //this.Event_Controller.CurrentConnectionInfo.MSN), String.Format("Set Major Alarm Process Status {0}", temp));
                    //}
                    //else
                    //{
                    //    if (tb1 != 38)
                    //    {
                    //        //Param_MajorAlarmProfile_obj.EventCode.Add(38);
                    //    }
                    //    if (tb2 != 39)
                    //    {
                    //        //Param_MajorAlarmProfile_obj.EventCode.Add(39);
                    //    }

                    //    Data_Access_Result temp = Param_Controller.SET_MajorAlarmProfile(Param_MajorAlarmProfile_obj);
                    //    ///MessageBox.Show(temp.ToString());

                    Notification Notifier = new Notification(String.Format("{0},Process Completed",
                        this.Event_Controller.CurrentConnectionInfo.MSN),
                        String.Format("Set Major Alarm Process Status {0}", temp));
                    //}
                }
                else
                {
                    Notification Notifier = new Notification(String.Format("Connect Meter"), String.Format("Please Connect Meter to Management"));
                    ////MessageBox.Show("Connect Meter First");
                }
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void bgw_SetMajorAlarms_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Progress_Bar.Enabled = false;
            //Progress_Bar.Visible = false;
            //v4.8.26
            AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(false);
        }

        private void bgw_GetMajorAlarms_DoWork(object sender, DoWorkEventArgs e)
        {
            Param_MajorAlarmProfile _Param_MajorAlarmProfile_obj = null;
            try
            {
                grid_Events.CurrentCell.Selected = false;
                grid_Events[0, 0].Selected = true;

                if (Application_Process.Is_Association_Developed)
                {
                    Application_Controller.IsIOBusy = true;

                    _Param_MajorAlarmProfile_obj = Param_MajorAlarmProfile_obj;

                    Param_Controller.GET_MajorAlarmProfile_Filter(ref _Param_MajorAlarmProfile_obj);
                    Param_Controller.GET_MajorAlarmProfile_AlarmStatus(ref _Param_MajorAlarmProfile_obj);

                    Notification Notifier = new Notification(String.Format("{0},Process Completed", this.Event_Controller.CurrentConnectionInfo.MSN), String.Format("GET Major Alarm Process Successful"));
                }
                else
                {
                    Notification Notifier = new Notification(String.Format("Connect Meter"), String.Format("Please Connect Meter to Management"));
                    ////MessageBox.Show("Connect Meter First");
                }
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
                if (_Param_MajorAlarmProfile_obj != null)
                    Param_MajorAlarmProfile_obj = _Param_MajorAlarmProfile_obj;
            }

        }

        private void bgw_GetMajorAlarms_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dictionary<int, string> temp = new Dictionary<int, string>();
            try
            {
                //Progress_Bar.Enabled = false;
                //Progress_Bar.Visible = false;
                //v4.8.26
                AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(false);

                ShowtoGUI_MajorAlarmProfile();
                //v4.8.29
                //BitArray alarmStatus = new BitArray(49);
                BitArray alarmStatus = new BitArray(Param_MajorAlarmProfile_obj.AlarmItems.Count);
                for (int i = 0; i < Param_MajorAlarmProfile_obj.AlarmItems.Count; i++)
                {
                    //temp.Add(Param_MajorAlarmProfile_obj.AlarmItems[i].Info.EventName, Param_MajorAlarmProfile_obj.AlarmItems[i].IsTriggered.ToString());
                    temp.Add(Param_MajorAlarmProfile_obj.AlarmItems[i].Info.EventCode, Param_MajorAlarmProfile_obj.AlarmItems[i].Info.EventName);

                    // if (Param_MajorAlarmProfile_obj.AlarmItems[i].IsMajorAlarm && Param_MajorAlarmProfile_obj.AlarmItems[i].IsTriggered)
                    if (Param_MajorAlarmProfile_obj.AlarmItems[i].IsTriggered)
                    {
                        alarmStatus[i] = true;
                    }
                    else
                    {
                        alarmStatus[i] = false;
                    }
                }
                if (!dbController.saveAlarmStatus(Application_Controller.ConnectionManager.ConnectionInfo.MSN, alarmStatus))
                {
                    Notification n = new Notification("Error", "Alarm Status not saved in database");
                }
                if (!dbController.save_majorAlarm(Application_Controller.ConnectionManager.ConnectionInfo.MSN, Param_MajorAlarmProfile_obj))
                {
                    Notification n = new Notification("Error", "Major Alarms not saved in database");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void bgw_SetAlarmStatus_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Data_Access_Result temp_1 = Param_Controller.SET_MajorAlarmProfile_Status(Param_MajorAlarmProfile_obj);
            }
            catch
            {
                throw;
            }
            try
            {
                //grid_Events[0, 0].Selected = true;
                // grid_Events.CurrentCell.Selected = false;
                Save_MajorAlarmProfile();
                if (Application_Process.Is_Association_Developed)
                {
                    Application_Controller.IsIOBusy = true;

                }
                else
                {
                    Notification Notifier = new Notification(String.Format("Connect Meter"), String.Format("Please Connect Meter to Management"));
                    ////MessageBox.Show("Connect Meter First");
                }
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void bgw_SetAlarmStatus_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Progress_Bar.Enabled = false;
            //Progress_Bar.Visible = false;

            //v4.8.26
            AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(false);
            Application_Controller.IsIOBusy = false;
        }

        private void bgw_getEventCautions_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Application_Process.Is_Association_Developed)
                {
                    Application_Controller.IsIOBusy = true;
                    //v4.8.31
                    //List_EventsCautions_toGET = new Param_EventsCaution[grid_Events.Rows.Count];
                    List_EventsCautions_toGET = new Param_EventsCaution[Param_MajorAlarmProfile_obj.AlarmItems.Count - 1]; //temp Azeem v5.3.12

                    for (int i = 0; i < List_EventsCautions_toGET.Length; i++)
                    {
                        List_EventsCautions_toGET[i] = new Param_EventsCaution();
                    }
                    Param_Controller.GET_EventsCaution(ref List_EventsCautions_toGET);
                    ShowtoGUI_Flag = true;
                    #region GET

                    foreach (var item in List_EventsCautions_toGET)
                    {
                        for (int i = 0; i < ListEventCautions.Count; i++)
                        {
                            if (ListEventCautions[i].Event_Code == item.Event_Code)
                            {
                                ListEventCautions[i].FlashTime = item.FlashTime;
                                ListEventCautions[i].CautionNumber = item.CautionNumber;
                                ListEventCautions[i].Flag = item.Flag;
                                //item.
                                //v4.8.31
                                //ListEventCautions[i].IsDisplayCaution = item.IsDisplayCaution;
                                //ListEventCautions[i].IsFlashCaution = item.IsFlashCaution;
                                //ListEventCautions[i].IsReadCaution = item.IsReadCaution;
                                break;
                            }

                        }
                    }

                    #endregion
                    Notification Notifier = new Notification(String.Format("{0},Process Completed",
                        this.Event_Controller.CurrentConnectionInfo.MSN),
                        String.Format("GET Flash Time Successful"));
                }
                else
                {
                    Notification Notifier = new Notification(String.Format("Connect Meter"),
                        String.Format("Please Connect Meter to Management"));
                }
            }
            finally
            {
                ShowtoGUI_Flag = false;
                Application_Controller.IsIOBusy = false;
            }
        }

        private void bgw_getEventCautions_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //Progress_Bar.Enabled = false;
                //Progress_Bar.Visible = false;

                //v4.8.26
                AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(false);
                ShowtoGUI_Flag = true;
                ShowtoGUI_EventCautions();
                ShowtoGUI_MajorAlarmProfile();

                dbController.save_CautionsAndFlash(Application_Controller.ConnectionManager.ConnectionInfo.MSN, ListEventCautions);
                //#region ShowToGUI
                //for (int i = 0; i < ListEventCautions.Count; i++)
                //{
                //    grid_Events[r_EventName, i].Value = ListEventCautions[i].Event_Name;
                //    grid_Events[r_EventCode, i].Value = ListEventCautions[i].Event_Code;
                //    grid_Events[r_Caution, i].Value = ListEventCautions[i].CausionNumber;
                //    grid_Events[r_FlashTime, i].Value = ListEventCautions[i].FlashTime;

                //    if (ListEventCautions[i].Flag % 2 == 1)
                //    {
                //        grid_Events[r_DisplayCaution, grid_Events.Rows.Count - 1].Value = true;
                //        Application.DoEvents();
                //    }
                //    else
                //    {
                //        grid_Events[r_DisplayCaution, grid_Events.Rows.Count - 1].Value = false;
                //        Application.DoEvents();
                //    }
                //    if ((ListEventCautions[i].Flag >> 2) % 2 == 1)
                //    {
                //        //display flash 
                //        grid_Events[r_IsFlash, grid_Events.Rows.Count - 1].Value = true;
                //        Application.DoEvents();
                //    }
                //    else
                //    {
                //        grid_Events[r_IsFlash, grid_Events.Rows.Count - 1].Value = false;
                //        Application.DoEvents();
                //    }

                //    if (ListEventCautions[i].EventId == MeterEvent.TimeBaseEvent_1 || ListEventCautions[i].EventId == MeterEvent.TimeBaseEvent_2)
                //    {
                //        grid_Events[r_Caution, grid_Events.Rows.Count - 1].ReadOnly = true;
                //        grid_Events[r_DisplayCaution, i].ReadOnly = true;
                //        grid_Events[r_FlashTime, i].ReadOnly = true;
                //        //         grid_Events[r_IsMajorAlarm, i].Value = true;
                //        //         grid_Events[r_IsMajorAlarm, i].ReadOnly = true;


                //    }
                //    else
                //    {
                //        grid_Events[r_Caution, i].ReadOnly = false;
                //        grid_Events[r_DisplayCaution, i].ReadOnly = false;
                //        grid_Events[r_FlashTime, i].ReadOnly = false;
                //        // grid_Events[r_IsMajorAlarm, i].Value = false;
                //        //  grid_Events[r_IsMajorAlarm, i].ReadOnly = false;


                //    }

                //}
                //#endregion
                Application.DoEvents();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void bgw_SetEventCautions_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Application_Process.Is_Association_Developed)
                {
                    Application_Controller.IsIOBusy = true;
                    //v4.8.31
                    //Param_EventsCaution[] List_EventsCautions_toSET = new Param_EventsCaution[grid_Events.Rows.Count];
                    Param_EventsCaution[] List_EventsCautions_toSET = new Param_EventsCaution[Param_MajorAlarmProfile_obj.AlarmItems.Count - 1]; //temp Azeem v5.3.12

                    int i = 0;
                    for (i = 0; i < List_EventsCautions_toSET.Length; i++)
                    {
                        List_EventsCautions_toSET[i] = new Param_EventsCaution();
                    }
                    foreach (Param_EventsCaution item in this.ListEventCautions)
                    {
                        try
                        {
                            if (item.EventId != null)
                            {
                                int index = (int)item.EventId.Value;
                                index--;
                                i = item.Event_Code;
                                if (index >= 0 && index < List_EventsCautions_toSET.Length)
                                {
                                    List_EventsCautions_toSET[index].eventCounter = item.eventCounter;
                                    List_EventsCautions_toSET[index].Event_Name = item.Event_Name;
                                    List_EventsCautions_toSET[index].EventId = item.EventId;
                                    List_EventsCautions_toSET[index].Event_Code = item.Event_Code;
                                    List_EventsCautions_toSET[index].CautionNumber = item.CautionNumber;
                                    List_EventsCautions_toSET[index].Flag = item.Flag;
                                    List_EventsCautions_toSET[index].FlashTime = item.FlashTime;

                                }
                            }
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                    Data_Access_Result temp = Param_Controller.SET_EventsCaution(List_EventsCautions_toSET);
                    Notification Notifier = new Notification(String.Format("{0},Process Completed", this.Event_Controller.CurrentConnectionInfo.MSN),
                        String.Format("Set Flash Time Process Status {0}", temp));
                }
                else
                {
                    Notification Notifier = new Notification(String.Format("Connect Meter"), String.Format("Please Connect Meter to Management"));
                    ////MessageBox.Show("Connect Meter First");
                }
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }

        }

        private void bgw_SetEventCautions_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Progress_Bar.Enabled = false;
            //Progress_Bar.Visible = false;
            //v4.8.26
            AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(false);
        }

        private void bgw_securityData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Progress_Bar.Enabled = false;
            //Progress_Bar.Visible = false;

            //v4.8.26
            AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(false);
            btn_ReadSecurityData.Enabled = true;
            displaySecurityReport(securityReportData);
        }

        private void bgw_securityData_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<EventInfo> eventsToRead = new List<EventInfo>();

                EventInfo eventItem = new EventInfo();
                //Login
                eventItem.EventCode = 109;
                eventsToRead.Add(eventItem);
                eventItem = new EventInfo();
                //MDI Reset 101
                eventItem.EventCode = 101;
                eventsToRead.Add(eventItem);
                eventItem = new EventInfo();
                //Param 102
                eventItem.EventCode = 102;
                eventsToRead.Add(eventItem);
                eventItem = new EventInfo();
                //Power Fail start
                eventItem.EventCode = 111;
                eventsToRead.Add(eventItem);

                Application_Controller.IsIOBusy = true;

                securityReportData = Event_Controller.ReadEventLogData(eventsToRead);

                if (!HidePrintReportButtons)
                {
                    //Get Customer Reference Code
                    Application_Controller.Param_Controller.GET_Customer_Reference(ref obj_CustomerCode);
                    //Get Active Season
                    Application_Controller.InstantaneousController.Get_Active_Season(Instantaneous_Class_obj);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        public void bgw_ReadLogBook_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Progress_Bar.Visible = false;
            //Progress_Bar.Enabled = false;

            //v4.8.26
            AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(false);
            showLogBook(TData);
            if (IsEventTimeCompensationRequired || check_E_addToDB.Checked)
            {
                dbController.saveLogBook(TData, Application_Controller.ConnectionManager.ConnectionInfo.MSN, IsEventTimeCompensationRequired);
            }
        }

        public void bgw_ReadLogBook_DoWork(object sender, DoWorkEventArgs e)
        {
            bool ReadByDateTime = chkReadByDateTime.Checked;
            if (IsEventTimeCompensationRequired)
            {
                ReadMonitoringTime();
            }
            EventInfo e_info = new EventInfo();
            e_info.EventCode = 0;
            e_info = Event_Controller.EventLogInfoList.Find((x) => x.EventCode == 0);

            if (ReadByDateTime)
            {
                TData = Event_Controller.ReadEventLogData(e_info, usDateRangeEv.dtpFrom.Value, usDateRangeEv.dtpTo.Value);
            }
            else
                TData = Event_Controller.ReadEventLogData(e_info);
            TData = Event_Controller.ReadEventLogData(e_info);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = true;
                List<EventInfo> listEventInfo_Selected = new List<EventInfo>();
                combo_Events_SelectedITems.Items.Clear();
                listEventInfo_Selected.Clear();
                foreach (EventInfo item in list_Event_SelectableEvents.CheckedItems)
                {
                    listEventInfo_Selected.Add(item);
                    combo_Events_SelectedITems.Items.Add(item); //fill list with selected Items
                }
                if (combo_Events_SelectedITems.Items.Count <= 0)
                {
                    return;
                }
                if (radio_Events_CountersOnly.Checked)
                {
                    //Get checked event count from meter
                    ListEventItem = new List<EventItem>();
                    foreach (var eventInfo in listEventInfo_Selected)
                    {
                        lbl_Status.BeginInvoke(new Action<string>(Update_Event_Status_Label)
                            , new String[] { eventInfo.EventName });
                        uint T = Event_Controller.Get_EventCounter_Internal(eventInfo);
                        EventItem EventItem_ = new EventItem();
                        EventItem_.EventInfo = eventInfo;
                        EventItem_.EventCounter = T;
                        ListEventItem.Add(EventItem_);
                    }

                }
                else
                {
                    if (IsEventTimeCompensationRequired) { ReadMonitoringTime(); }

                    foreach (var selectedEvent in listEventInfo_Selected)
                    {
                        if (selectedEvent is EventLogInfo)
                        {
                            ((EventLogInfo)selectedEvent).PreviousEventCounter = (EventReadCount > 0) ?
                                (EventReadCount * -1) : EventLogInfo.EventCounterNotApplied;
                            ((EventLogInfo)selectedEvent).CurrentEventCounter = EventLogInfo.EventCounterNotApplied;
                        }
                    }
                    ///Reading Event Data
                    foreach (var eventInfo in listEventInfo_Selected)
                    {


                        lbl_Status.BeginInvoke(new Action<string>(Update_Event_Status_Label)
                            , new String[] { eventInfo.EventName });
                        TData = Event_Controller.ReadEventLogData(eventInfo);
                        tempEventLogs.Add(TData);
                    }
                    for (int i = 0; i < tempEventLogs.Count; i++)
                    {
                        EventData CurrentItem = listEventData.Find((x) => x.EventInfo.EventCode == tempEventLogs[i].EventInfo.EventCode);
                        listEventData.Remove(CurrentItem);
                        listEventData.Add(tempEventLogs[i]);
                    }
                    if (!HidePrintReportButtons)
                    {
                        //Get Customer Reference Code
                        Application_Controller.Param_Controller.GET_Customer_Reference(ref obj_CustomerCode);
                        //Get Active Season
                        Application_Controller.InstantaneousController.Get_Active_Season(Instantaneous_Class_obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #region OLD CODE
            //try
            //{
            //    List<comm.EventInfo> listEventInfo_Selected = new List<comm.EventInfo>();
            //    foreach (comm.EventInfo item in list_Event_SelectableEvents.CheckedItems)
            //    {
            //        listEventInfo_Selected.Add(item);
            //    }
            //    if (radio_Events_CountersOnly.Checked)
            //    {
            //        //Get checked event count from meter
            //        ListEventItem = Event_Controller.Get_EventLog_Counters_Internal(listEventInfo_Selected);
            //    }
            //    else
            //    {
            //        List<EventData> tempEventLogs = Event_Controller.ReadEventLogData(listEventInfo_Selected);
            //        if (listEventData != null)
            //        {
            //            listEventData = new List<EventData>();
            //        }
            //        ///combo_Events_SelectedITems.Items.Clear();
            //        foreach (EventData eventLogItem in tempEventLogs)
            //        {
            //            EventData eventLog = listEventData.Find((x) => x.EventInfo.EventCode == eventLogItem.EventInfo.EventCode);
            //            if (eventLog != null)
            //            {
            //                listEventData.Remove(eventLog);
            //            }
            //            listEventData.Add(eventLogItem);
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //} 
            #endregion
        }

        private void ReadMonitoringTime()
        {
            var _Param_Monitoring_Time_Object = ParamConfigurationSet.ParamMonitoringTime;// Param_Monitoring_Time_Object;
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Monitoring Times");

            Param_Controller.GET_MT_CT_Fail(ref _Param_Monitoring_Time_Object);
            Param_Controller.GET_MT_High_Neutral_Current(ref _Param_Monitoring_Time_Object);
            Param_Controller.GET_MT_Imbalance_Volt(ref _Param_Monitoring_Time_Object);
            Param_Controller.GET_MT_Over_Current(ref _Param_Monitoring_Time_Object);
            Param_Controller.GET_MT_Over_Load(ref _Param_Monitoring_Time_Object);
            Param_Controller.GET_MT_Over_Volt(ref _Param_Monitoring_Time_Object);
            Param_Controller.GET_MT_Phase_Fail(ref _Param_Monitoring_Time_Object);
            Param_Controller.GET_MT_Phase_Sequence(ref _Param_Monitoring_Time_Object);
            Param_Controller.GET_MT_PT_Fail(ref _Param_Monitoring_Time_Object);
            Param_Controller.GET_MT_Reverse_Energy(ref _Param_Monitoring_Time_Object);
            Param_Controller.GET_MT_Reverse_Polarity(ref _Param_Monitoring_Time_Object);
            Param_Controller.GET_MT_Tamper_Energy(ref _Param_Monitoring_Time_Object);
            Param_Controller.GET_MT_Under_Volt(ref _Param_Monitoring_Time_Object);
            //Param_Controller.GET_MT_Start_Generator(ref _Param_Monitoring_Time_Object);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //groupBox1.Enabled = true;
                //progressBar1.Enabled = false;
                //progressBar1.Visible = false;
                AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(false);
                if (e.Error != null)
                {
                    throw e.Error;
                }
                if (combo_Events_SelectedITems.Items.Count <= 0)
                {
                    Notification Notifier = new Notification("Select Events", "Please Select any single Event to read data");

                }
                //if (radio_Events_CountersOnly.Checked)
                //{
                //    showEventCountersToGrid();
                //}
                //else
                //{
                //    combo_Events_SelectedITems.SelectedIndex = 0;
                //}
                //database
                foreach (EventData EventData_obj in listEventData)
                {
                    if (EventData_obj.EventRecords.Count > 0)
                    {
                        EventData_obj.EventRecords.Sort((x, y) => y.EventCounter.CompareTo(x.EventCounter));
                        EventData savedEventsData = null;

                        if (IsEventTimeCompensationRequired)
                        {
                            savedEventsData = dbController.ReadSavedEventData(
                            EventData_obj.EventInfo.EventCode,
                            Application_Controller.ConnectionManager.ConnectionInfo.MSN,
                            EventData_obj.EventRecords[EventData_obj.EventRecords.Count - 1].EventCounter,
                            EventData_obj.EventRecords[0].EventCounter);
                        }
                        if (IsEventTimeCompensationRequired)
                        {
                            for (int index = 0; index < EventData_obj.EventRecords.Count; index++)
                            {
                                EventItem eItem = EventData_obj.EventRecords[index];

                                DateTime eventTime = CaptureDateTime_Compensate(eItem.EventInfo.EventCode, eItem.EventDateTimeStamp);

                                EventItem savedItem = savedEventsData.EventRecords.Find(x => x.EventCounter == eItem.EventCounter);

                                if (savedItem != null) eventTime = savedItem.EventDateTimeStamp;
                                EventData_obj.EventRecords[index].EventDateTimeStamp = eventTime;
                            }
                        }

                        if (IsEventTimeCompensationRequired || check_E_addToDB.Checked)
                        {
                            dbController.saveEventData(EventData_obj, Application_Controller.ConnectionManager.ConnectionInfo.MSN, IsEventTimeCompensationRequired);
                        }
                    }
                }
                //Update grid after Data insertion in DB by Azeem //v4.8.12
                if (radio_Events_CountersOnly.Checked)
                {
                    showEventCountersToGrid();
                }
                else
                {
                    if (combo_Events_SelectedITems.Items.Count > 0)
                        combo_Events_SelectedITems.SelectedIndex = 0;
                }
                Notification Notifier_1 = new Notification("Event Reading Complete", "Event data read completed successfully");
            }
            catch (Exception ex)
            {
                ///MessageBox.Show(this, ex.Message);
                Notification Notifier = new Notification("Error Notifying Event Data", String.Format("Error reading event data\r\n{0}", ex.Message));
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
                //progressBar1.Visible = false;
                AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(false);
                lbl_Status.Visible = false;
            }
        }

        #endregion

        private void btn_SecurityReport_Click(object sender, EventArgs e)
        {
            displaySecurityReport(listEventData);
        }

        private void btn_ReadSecurityData_Click(object sender, EventArgs e)
        {
            ApplyRights_IsEventCompensationRequired();
            btn_ReadSecurityData.Enabled = false;
            bgw_securityData.RunWorkerAsync();

            //Progress_Bar.Visible = true;
            //Progress_Bar.Enabled = true;
            //v4.8.26
            AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(true);
        }

        private void btn_ReadLogBook_Click(object sender, EventArgs e)
        {
            if (Application_Controller.Applicationprocess_Controller.IsConnected)
            {
                //Progress_Bar.Visible = true;
                //Progress_Bar.Enabled = true;
                //v4.8.26
                AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(true);
                ApplyRights_IsEventCompensationRequired();
                bgw_ReadLogBook.RunWorkerAsync();
            }
            else
            {
                Notification n = new Notification("Error", "Application not connected to meter");
            }
        }

        private void btn_SetMajorStatus_Click(object sender, EventArgs e)
        {
            //Progress_Bar.Visible = true;
            //Progress_Bar.Enabled = true;

            //v4.8.26
            AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(true);
            Application_Controller.IsIOBusy = true;
            bgw_SetAlarmStatus.RunWorkerAsync();
        }

        private void btn_SET_Click(object sender, EventArgs e)
        {
            bgw_SetEventCautions.RunWorkerAsync();

            //Progress_Bar.Visible = true;
            //Progress_Bar.Enabled = true;
            //v4.8.26
            AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(true);
        }

        private void btn_SetMajorAlarm_Click(object sender, EventArgs e)
        {
            //Progress_Bar.Enabled = true;
            //Progress_Bar.Visible = true;

            //v4.8.26
            AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(true);
            bgw_SetMajorAlarms.RunWorkerAsync();
        }

        private void btn_GetMajorAlarm_Click(object sender, EventArgs e)
        {
            //Progress_Bar.Enabled = true;
            //Progress_Bar.Visible = true;

            //v4.8.26
            AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(true);
            bgw_GetMajorAlarms.RunWorkerAsync();
        }

        private void btn_GET_EventsCautions_Click(object sender, EventArgs e)
        {
            bgw_getEventCautions.RunWorkerAsync();

            //Progress_Bar.Visible = true;
            //Progress_Bar.Enabled = true;
            //v4.8.26
            AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(true);

        }

        private void btn_Events_GET_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorker1.IsBusy)
                {
                    ///MessageBox.Show(this, "Please wait till previous read process completes");
                    Notification Notifier = new Notification("Please Wait", "Please wait till previous read process completes");
                    return;
                }
                if (Application_Process.Is_Association_Developed)
                {
                    ApplyRights_IsEventCompensationRequired();
                    Application_Controller.IsIOBusy = true;
                    EventReadCount = ReadLastNumberOfEvent();
                    ///groupBox1.Enabled = false;
                    backgroundWorker1.RunWorkerAsync();

                    //progressBar1.Enabled = true;
                    //progressBar1.Visible = true;
                    //progressBar1.ForeColor = Color.Aqua;
                    //v4.8.26
                    AccurateOptocomSoftware.Common.clsStatus.ProgBarVisible(true);
                    if (combo_Events_SelectedITems.Items.Count > 0)
                        combo_Events_SelectedITems.SelectedIndex = combo_Events_SelectedITems.Items.Count - 1;
                }
                else
                {
                    ///MessageBox.Show("Connect to Meter!!");
                    Notification Notifier = new Notification("Error Reading", "Please Connect to Meter");
                }
            }
            catch (Exception ex)
            {
                /// MessageBox.Show(this, ex.Message);
                Notification Notifier = new Notification("Error Reading Events", String.Format("Error Reading Events Data\r\n{0}", ex.Message));
            }

        }

        private void btn_Reports_events_Click(object sender, EventArgs e)
        {
            /*
             * Comments Added by M.Azeem Inayat with the help of code reading
             */
            string EventTotalCount = "0";
            if (Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count > 0)
                Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Clear();

            MeterConfig meter_type_info = application_Controller.ConnectionController.SelectedMeter;
            ReportViewer rpt = null;
            int reportType = 0;
            //LogBook Report Added by Azeem Inatyat
            if (tb_Events.SelectedTab.Name == "tab_LogBook")
            {
                if (Dataset_Events.DataTable_Meter_Events.Rows.Count > 0)
                    Dataset_Events.DataTable_Meter_Events.Rows.Clear();

                //bool IsWebFormat = false;  //v4.8.31 Commented

                if (grid_LogBook.Rows.Count <= 0) return;

                int FirstRowCount = (Convert.ToInt32(grid_LogBook.Rows[0].Cells["EventCounters"].Value));
                int LastRowCount = (Convert.ToInt32(grid_LogBook.Rows[grid_LogBook.Rows.Count - 1].Cells["EventCounters"].Value));
                EventTotalCount = ((FirstRowCount > LastRowCount) ? FirstRowCount : LastRowCount).ToString();
                //EventTotalCount = ((Convert.ToInt32(grid_LogBook.Rows[0].Cells["EventCounters"].Value)) > (Convert.ToInt32(grid_LogBook.Rows[grid_LogBook.Rows.Count - 1].Cells["EventCounters"].Value))) ?
                //    (grid_LogBook.Rows[0].Cells["EventCounters"].Value.ToString()) : (grid_LogBook.Rows[grid_LogBook.Rows.Count - 1].Cells["EventCounters"].Value.ToString());
                //int temp;
                //var MaxID2 = grid_LogBook.Rows.Cast<DataGridViewRow>()
                //            .Max(r => int.TryParse(r.Cells["Id"].Value.ToString(), out temp) ?
                //                       temp : 0);

                foreach (DataGridViewRow row in grid_LogBook.Rows)
                {
                    Dataset_Events.DataTable_Meter_Events.Rows.Add();
                    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_date =
                        Convert.ToString(row.Cells["DateTimes"].Value);
                    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_name =
                        Convert.ToString(row.Cells["EventNames"].Value);
                    if (LocalCommon.GetReportFormat(application_Controller.CurrentUser.CurrentAccessRights) == ReportFormat.WAPDA_DDS) // IsWapdaFormat)
                    {
                        Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_code = "";
                        Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_counter = "";
                    }
                    else
                    {
                        Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_code =
                                        Convert.ToString(row.Cells["EventCodes"].Value);
                        Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_counter =
                            Convert.ToString(row.Cells["EventCounters"].Value);
                    }

                    Dataset_Events.DataTable_Meter_Events[Dataset_Events.DataTable_Meter_Events.Rows.Count - 1].event_detail = "";
                }
                // Commented by Azeem Inayat
                //rpt = new ReportViewer(Dataset_Events.DataTable_Meter_Events, Event_Controller.CurrentConnectionInfo.MSN,
                //    Event_Controller.CurrentConnectionInfo.MeterInfo.Model, "Log Book (Events Report)");
                if (LocalCommon.GetReportFormat(application_Controller.CurrentUser.CurrentAccessRights) == ReportFormat.WEB_GALAXY) //IsWebFormat)
                {
                    //RPT_Events
                    rpt = new ReportViewer(Dataset_Events.DataTable_Meter_Events, Event_Controller.CurrentConnectionInfo.MSN, "",
                        Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(),
                        meter_type_info.MeterModel, 3, meter_type_info);
                }
                else
                {
                    reportType = 1;
                    //RPT_EventsAll
                    rpt = new ReportViewer(Dataset_Events.DataTable_Meter_Events, Event_Controller.CurrentConnectionInfo.MSN,
                        obj_CustomerCode.Customer_Code_String, Application_Controller.Applicationprocess_Controller.UserId.ToString(),
                        Instantaneous_Class_obj.Active_Season.ToString(), meter_type_info, reportType, application_Controller.CurrentUser.CurrentAccessRights,
                        EventTotalCount);
                    //, ((IsWapdaFormat) ? ReportFormat.WAPDA_DDS : ((IsWebFormat)?ReportFormat.WEB_GALAXY: ReportFormat.ADVANCE_MTI)));
                }
            }
            else if (tb_Events.SelectedTab.Name == "Individual_Events" || tb_Events.SelectedTab.Name == "Meter_Events") //tb_Events.SelectedIndex == 0)
            {
                //Report Type 1 if Single Event Report
                //Report type 2 if Both Start and End on Same report
                reportType = getMeterEventsReportData();
                if (radio_Events_CountersOnly.Checked)
                {
                    reportType = 1;
                    //RPT_EventsAll is used to display All event counters
                    rpt = new ReportViewer(Dataset_Events.DataTable_Meter_Events, Event_Controller.CurrentConnectionInfo.MSN,
                        obj_CustomerCode.Customer_Code_String, Application_Controller.Applicationprocess_Controller.UserId.ToString(),
                        Instantaneous_Class_obj.Active_Season.ToString(), meter_type_info, reportType, application_Controller.CurrentUser.CurrentAccessRights,
                        EventTotalCount);
                    //, ((IsWapdaFormat) ? ReportFormat.WAPDA_DDS : ((IsWebFormat) ? ReportFormat.WEB_GALAXY : ReportFormat.ADVANCE_MTI)));

                    #region commented in v4.8.23
                    ////RPT_Events
                    //rpt = new ReportViewer(Dataset_Events.DataTable_Meter_Events, Event_Controller.CurrentConnectionInfo.MSN, "",
                    //    Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(),
                    //    Event_Controller.CurrentConnectionInfo.MeterInfo.Model, 3, meter_type_info);
                    #endregion

                    // Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString()
                    //datatable, MSN, customerCode, pid, active_season, Model, report_type, meter_type_info)
                    //datatable, string MSN, string Model, string title, string customerCode, string pid, string active_season, Configs.meter_type_infoRow meter_type_info

                }
                else if (radio_Events_CompleteLog.Checked)
                {
                    //
                    //rpt = new ReportViewer(Dataset_Events.DataTable_Meter_Events, Event_Controller.CurrentConnectionInfo.MSN,
                    //    Event_Controller.CurrentConnectionInfo.MeterInfo.Model, combo_Events_SelectedITems.Text);

                    //rpt = new ReportViewer(Dataset_Events.DataTable_Meter_Events, Event_Controller.CurrentConnectionInfo.MSN,
                    //    Event_Controller.CurrentConnectionInfo.MeterInfo.Model, combo_Events_SelectedITems.Text,
                    //obj_CustomerCode.Customer_Code_String, Application_Controller.Applicationprocess_Controller.UserId.ToString(), Instantaneous_Class_obj.Active_Season.ToString());
                    //
                    if (reportType == 3) //Single Event (only start or only End)
                    {
                        //RPT_EventsAll
                        string counterColName = Dataset_Events.DataTable_Meter_Events.event_counterColumn.ColumnName;
                        int lastRowIndex = Dataset_Events.DataTable_Meter_Events.Rows.Count - 1;
                        int FirstRowCount = (Convert.ToInt32(Dataset_Events.DataTable_Meter_Events.Rows[0][counterColName]));
                        int LastRowCount = (Convert.ToInt32(Dataset_Events.DataTable_Meter_Events.Rows[lastRowIndex][counterColName]));
                        EventTotalCount = ((FirstRowCount > LastRowCount) ? FirstRowCount : LastRowCount).ToString();


                        //EventTotalCount = ((Convert.ToInt32(Dataset_Events.DataTable_Meter_Events.Rows[0][counterColName])) >
                        //                   (Convert.ToInt32(Dataset_Events.DataTable_Meter_Events.Rows[lastRowIndex][counterColName]))) ?
                        //                   (Dataset_Events.DataTable_Meter_Events.Rows[0][counterColName].ToString()) :
                        //                   (Dataset_Events.DataTable_Meter_Events.Rows[lastRowIndex][counterColName].ToString());

                        rpt = new ReportViewer(Dataset_Events.DataTable_Meter_Events, Event_Controller.CurrentConnectionInfo.MSN,
                        obj_CustomerCode.Customer_Code_String, Application_Controller.Applicationprocess_Controller.UserId.ToString(),
                        Instantaneous_Class_obj.Active_Season.ToString(), meter_type_info, reportType, application_Controller.CurrentUser.CurrentAccessRights,
                        EventTotalCount);
                        //, ((IsWapdaFormat) ? ReportFormat.WAPDA_DDS : ((IsWebFormat) ? ReportFormat.WEB_GALAXY : ReportFormat.ADVANCE_MTI)));
                    }
                    else if (reportType == 2) //Both Start and End on same report
                    {
                        //RPT_EventsAll2
                        string counterColName = Dataset_Events.DataTable_Meter_Events_With_Recovery.event_counter_OccurrenceColumn.ColumnName;
                        int lastRowIndex = Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows.Count - 1;
                        EventTotalCount = ((Convert.ToInt32(Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows[0][counterColName])) >
                                           (Convert.ToInt32(Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows[lastRowIndex][counterColName]))) ?
                                           (Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows[0][counterColName].ToString()) :
                                           (Dataset_Events.DataTable_Meter_Events_With_Recovery.Rows[lastRowIndex][counterColName].ToString());

                        rpt = new ReportViewer(Dataset_Events.DataTable_Meter_Events_With_Recovery, Event_Controller.CurrentConnectionInfo.MSN,
                        obj_CustomerCode.Customer_Code_String, Application_Controller.Applicationprocess_Controller.UserId.ToString(),
                        Instantaneous_Class_obj.Active_Season.ToString(), meter_type_info, reportType, application_Controller.CurrentUser.CurrentAccessRights,
                        EventTotalCount);
                        //, ((IsWapdaFormat) ? ReportFormat.WAPDA_DDS : ((IsWebFormat) ? ReportFormat.WEB_GALAXY : ReportFormat.ADVANCE_MTI)));
                    }
                }
            }
            else if (tb_Events.SelectedTab.Name == "ProgramEvents") //tb_Events.SelectedIndex == 1)
            {
                getMajorAlarmReportData();
                //RPT_MAJOR_ALARM
                rpt = new ReportViewer(
                    Dataset_Events.DataTable_Major_Alarms,
                    Event_Controller.CurrentConnectionInfo.MSN, "", Application_Controller.Applicationprocess_Controller.UserId,
                    Instantaneous_Class_obj.Active_Season.ToString(),
                    meter_type_info.MeterModel,
                    1,
                    meter_type_info);
            }
            rpt.Show();
        }

        private void check_resetAll_Alarms_Click(object sender, EventArgs e)
        {
            if (check_resetAll_Alarms.Checked)
            {
                for (int i = 0; i < Param_MajorAlarmProfile_obj.AlarmItems.Count; i++)
                {
                    if (Param_MajorAlarmProfile_obj.AlarmItems[i].IsTriggered)
                    {
                        Param_MajorAlarmProfile_obj.AlarmItems[i].IsReset = true;
                    }
                }

            }

            else
            {
                for (int i = 0; i < Param_MajorAlarmProfile_obj.AlarmItems.Count; i++)
                {
                    if (Param_MajorAlarmProfile_obj.AlarmItems[i].IsTriggered)
                    {
                        Param_MajorAlarmProfile_obj.AlarmItems[i].IsReset = false;
                    }
                }
            }
            ShowtoGUI_MajorAlarmProfile();
        }

        private void check_E_addToDB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void displaySecurityReport(List<EventData> listEventData)
        {
            ds_security dataset_securiry = new ds_security();

            //List<EventData> list_security = new List<EventData>();
            //List<security> toShow = new List<security>();
            security obj_security = new security();
            if (listEventData != null)
            {
                //foreach (EventData item in listEventData)
                //{
                //    if (item.EventInfo.EventCode.Equals(111) || item.EventInfo.EventCode.Equals(102) ||
                //        item.EventInfo.EventCode.Equals(101) || item.EventInfo.EventCode.Equals(109))
                //    {
                //        list_security.Add(item);
                //    }
                //}


                ////Now we have EMS LOGIN, MDI RESET PARAMETERIZATION and POWER FAILURE in security list
                //foreach (EventData item in listEventData)
                //{
                //    if (item.EventInfo.EventCode.Equals(111) || item.EventInfo.EventCode.Equals(102) ||
                //        item.EventInfo.EventCode.Equals(101) || item.EventInfo.EventCode.Equals(109))
                //    {
                //        item.EventRecords.Sort((x, y) => x.EventCounter.CompareTo(y.EventCounter));
                //        int indx = item.EventRecords.Count - 1;
                //        //after sorting, we get the latest record at Index=item.EventRecords.Count-1;
                //        obj_security.EventCode = item.EventRecords[indx].EventInfo.EventCode;
                //        obj_security.EventName = item.EventRecords[indx].EventInfo.EventName;
                //        obj_security.EventCounter = item.EventRecords[indx].EventCounter;
                //        obj_security.EventLastOccuranceDate = item.EventRecords[indx].EventDateTimeStamp;
                //        obj_security.MaxCounter = item.EventRecords.Count;
                //        obj_security.EventDetail = item.EventRecords[indx].EventDetailString;

                //        toShow.Add(obj_security);
                //        obj_security = new security();
                //    }
                //}



                foreach (EventData item in listEventData)
                {
                    obj_security = new security();
                    item.EventRecords.Sort((x, y) => x.EventCounter.CompareTo(y.EventCounter));
                    int indx = item.EventRecords.Count - 1;
                    //after sorting, we get the latest record at Index=item.EventRecords.Count-1;
                    obj_security.EventCode = item.EventRecords[indx].EventInfo.EventCode;
                    obj_security.EventName = item.EventRecords[indx].EventInfo.EventName;
                    obj_security.EventCounter = item.EventRecords[indx].EventCounter;
                    obj_security.EventLastOccuranceDate = item.EventRecords[indx].EventDateTimeStamp;
                    obj_security.MaxCounter = item.EventRecords.Count;
                    obj_security.EventDetail = item.EventRecords[indx].EventDetailString;

                    //toShow.Add(obj_security);

                    int GridRowIndex = 0;
                    if (obj_security.EventCode == 109) //EventName.Equals("Software Login") || item.EventName.Equals("EMS Login"))
                    {
                        dataset_securiry.dataTable_Security.Rows.Add();
                        GridRowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                        dataset_securiry.dataTable_Security[GridRowIndex].Name = "Last OPTOCOM Communication Date and Time";
                        dataset_securiry.dataTable_Security[GridRowIndex].Value = obj_security.EventLastOccuranceDate.ToString(LocalCommon.DateTimeFormat);

                        dataset_securiry.dataTable_Security.Rows.Add();
                        GridRowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                        dataset_securiry.dataTable_Security[GridRowIndex].Name = "No. of OPTOCOM Communication";
                        dataset_securiry.dataTable_Security[GridRowIndex].Value = obj_security.EventCounter.ToString();

                    }
                    else if (obj_security.EventCode == 101) //item.EventName.Equals("MDI Reset"))
                    {
                        dataset_securiry.dataTable_Security.Rows.Add();
                        GridRowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                        dataset_securiry.dataTable_Security[GridRowIndex].Name = "Last Demand Reset Date and Time";
                        dataset_securiry.dataTable_Security[GridRowIndex].Value = obj_security.EventLastOccuranceDate.ToString(LocalCommon.DateTimeFormat);

                        dataset_securiry.dataTable_Security.Rows.Add();
                        GridRowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                        dataset_securiry.dataTable_Security[GridRowIndex].Name = "No. of Demand Resets";
                        dataset_securiry.dataTable_Security[GridRowIndex].Value = obj_security.EventCounter.ToString();

                    }
                    else if (obj_security.EventCode == 102) //item.EventName.Equals("Parameterization"))
                    {
                        dataset_securiry.dataTable_Security.Rows.Add();
                        GridRowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                        dataset_securiry.dataTable_Security[GridRowIndex].Name = "Last Programming Date and Time";
                        dataset_securiry.dataTable_Security[GridRowIndex].Value = obj_security.EventLastOccuranceDate.ToString(LocalCommon.DateTimeFormat);

                        //Resolved by Azeem Inayat
                        string LastProgrammerId = "00";
                        try
                        {
                            byte[] userId = new byte[] { obj_security.EventDetail[2], obj_security.EventDetail[3] };
                            Array.Reverse(userId);
                            //for SEAC7
                            //LastProgrammerId = (BitConverter.ToInt16(userId, 0).ToString()) + 
                            //        " " + Application_Controller.Applicationprocess_Controller.UserId.ToString();

                            //for Accurate Optocom Software
                            //Index 0 & 1 contains Application Id of Application
                            //Index 2 & 3 contains User Id of that Application

                            string evntDtl = "";
                            foreach (var b in obj_security.EventDetail)
                            {
                                evntDtl += (b.ToString("00") + " ");
                            }
                            LastProgrammerId = evntDtl; //BitConverter.ToInt16(userId, 0).ToString();
                        }
                        catch (Exception) { }

                        dataset_securiry.dataTable_Security.Rows.Add();
                        GridRowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                        dataset_securiry.dataTable_Security[GridRowIndex].Name = "ID of Last Programmer";
                        dataset_securiry.dataTable_Security[GridRowIndex].Value = LastProgrammerId;
                        //Application_Controller.Applicationprocess_Controller.UserId.ToString();


                        dataset_securiry.dataTable_Security.Rows.Add();
                        GridRowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                        dataset_securiry.dataTable_Security[GridRowIndex].Name = "No. of Programming";
                        dataset_securiry.dataTable_Security[GridRowIndex].Value = obj_security.EventCounter.ToString();

                    }
                    else if (obj_security.EventCode == 111) //item.EventName.Equals("Power Failure Start") || item.EventName.Equals("Power Outage"))
                    {
                        dataset_securiry.dataTable_Security.Rows.Add();
                        GridRowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                        dataset_securiry.dataTable_Security[GridRowIndex].Name = "Last Power Outage Date and Time";
                        dataset_securiry.dataTable_Security[GridRowIndex].Value = obj_security.EventLastOccuranceDate.ToString(LocalCommon.DateTimeFormat);

                        dataset_securiry.dataTable_Security.Rows.Add();
                        GridRowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                        dataset_securiry.dataTable_Security[GridRowIndex].Name = "No. of Power Outages";
                        dataset_securiry.dataTable_Security[GridRowIndex].Value = obj_security.EventCounter.ToString();

                    }
                }


                ////Now we have EMS LOGIN, MDI RESET PARAMETERIZATION and POWER FAILURE in security list
                //foreach (EventData item in list_security)
                //{
                //    item.EventRecords.Sort((x, y) => x.EventCounter.CompareTo(y.EventCounter));
                //    int indx = item.EventRecords.Count - 1;
                //    //after sorting, we get the latest record at Index=item.EventRecords.Count-1;
                //    obj_security.EventCode = item.EventRecords[indx].EventInfo.EventCode;
                //    obj_security.EventName = item.EventRecords[indx].EventInfo.EventName;
                //    obj_security.EventCounter = item.EventRecords[indx].EventCounter;
                //    obj_security.EventLastOccuranceDate = item.EventRecords[indx].EventDateTimeStamp;
                //    obj_security.MaxCounter = item.EventRecords.Count;
                //    obj_security.EventDetail = item.EventRecords[indx].EventDetailString;

                //    toShow.Add(obj_security);
                //    obj_security = new security();
                //}
                //foreach (var item in toShow)
                //{
                //    int rowIndex = 0;
                //    if (item.EventCode == 109) //EventName.Equals("Software Login") || item.EventName.Equals("EMS Login"))
                //    {
                //        dataset_securiry.dataTable_Security.Rows.Add();
                //        rowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                //        dataset_securiry.dataTable_Security[rowIndex].Name = "Last OPTOCOM Communication Date and Time";
                //        dataset_securiry.dataTable_Security[rowIndex].Value = item.EventLastOccuranceDate.ToString(LocalCommon.DateTimeFormat);

                //        dataset_securiry.dataTable_Security.Rows.Add();
                //        rowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                //        dataset_securiry.dataTable_Security[rowIndex].Name = "No. of OPTOCOM Communication";
                //        dataset_securiry.dataTable_Security[rowIndex].Value = item.EventCounter.ToString();

                //    }
                //    else if (item.EventCode == 101) //item.EventName.Equals("MDI Reset"))
                //    {
                //        dataset_securiry.dataTable_Security.Rows.Add();
                //        rowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                //        dataset_securiry.dataTable_Security[rowIndex].Name = "Last Demand Reset Date and Time";
                //        dataset_securiry.dataTable_Security[rowIndex].Value = item.EventLastOccuranceDate.ToString(LocalCommon.DateTimeFormat);

                //        dataset_securiry.dataTable_Security.Rows.Add();
                //        rowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                //        dataset_securiry.dataTable_Security[rowIndex].Name = "No. of Demand Resets";
                //        dataset_securiry.dataTable_Security[rowIndex].Value = item.EventCounter.ToString();

                //    }
                //    else if (item.EventCode == 102) //item.EventName.Equals("Parameterization"))
                //    {
                //        dataset_securiry.dataTable_Security.Rows.Add();
                //        rowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                //        dataset_securiry.dataTable_Security[rowIndex].Name = "Last Programming Date and Time";
                //        dataset_securiry.dataTable_Security[rowIndex].Value = item.EventLastOccuranceDate.ToString(LocalCommon.DateTimeFormat);

                //        //Resolved by Azeem Inayat
                //        string LastProgrammerId = "00";
                //        try
                //        {
                //            byte[] userId = new byte[] { item.EventDetail[2], item.EventDetail[3] };
                //            Array.Reverse(userId);
                //            //for SEAC7
                //            //LastProgrammerId = (BitConverter.ToInt16(userId, 0).ToString()) + 
                //            //        " " + Application_Controller.Applicationprocess_Controller.UserId.ToString();

                //            //for Accurate Optocom Software
                //            //Index 0 & 1 contains Application Id of Application
                //            //Index 2 & 3 contains User Id of that Application

                //            string evntDtl = "";
                //            foreach (var b in item.EventDetail)
                //            {
                //                evntDtl += (b.ToString("00") + " ");
                //            }
                //            LastProgrammerId = evntDtl; //BitConverter.ToInt16(userId, 0).ToString();
                //        }
                //        catch (Exception) { }

                //        dataset_securiry.dataTable_Security.Rows.Add();
                //        rowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                //        dataset_securiry.dataTable_Security[rowIndex].Name = "ID of Last Programmer";
                //        dataset_securiry.dataTable_Security[rowIndex].Value = LastProgrammerId;
                //        //Application_Controller.Applicationprocess_Controller.UserId.ToString();


                //        dataset_securiry.dataTable_Security.Rows.Add();
                //        rowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                //        dataset_securiry.dataTable_Security[rowIndex].Name = "No. of Programming";
                //        dataset_securiry.dataTable_Security[rowIndex].Value = item.EventCounter.ToString();

                //    }
                //    else if (item.EventCode == 111) //item.EventName.Equals("Power Failure Start") || item.EventName.Equals("Power Outage"))
                //    {
                //        dataset_securiry.dataTable_Security.Rows.Add();
                //        rowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                //        dataset_securiry.dataTable_Security[rowIndex].Name = "Last Power Outage Date and Time";
                //        dataset_securiry.dataTable_Security[rowIndex].Value = item.EventLastOccuranceDate.ToString(LocalCommon.DateTimeFormat);

                //        dataset_securiry.dataTable_Security.Rows.Add();
                //        rowIndex = dataset_securiry.dataTable_Security.Rows.Count - 1;
                //        dataset_securiry.dataTable_Security[rowIndex].Name = "No. of Power Outages";
                //        dataset_securiry.dataTable_Security[rowIndex].Value = item.EventCounter.ToString();

                //    }
                //}
                //dataset_securiry.dataTable_Security.Rows.Add();
                //dataset_securiry.dataTable_Security[dataset_securiry.dataTable_Security.Rows.Count - 1].Name =
                //    "ID of Last Programmer";
                //dataset_securiry.dataTable_Security[dataset_securiry.dataTable_Security.Rows.Count - 1].Value =
                //    Application_Controller.Applicationprocess_Controller.UserId.ToString();
                if (dataset_securiry.dataTable_Security.Rows.Count > 0) //v5.3.12
                {
                    MeterConfig meter_type_info = application_Controller.ConnectionController.SelectedMeter;
                    ReportViewer rpt = new ReportViewer(dataset_securiry, Event_Controller.CurrentConnectionInfo.MSN.ToString(), meter_type_info,
                        obj_CustomerCode._Customer_Code_String,
                        Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(),
                        application_Controller.CurrentUser.CurrentAccessRights); //,
                    //((IsWapdaFormat) ? ReportFormat.WAPDA_DDS : ((IsWebFormat) ? ReportFormat.WEB_GALAXY : ReportFormat.ADVANCE_MTI)));
                    rpt.Show();
                }
            }
        }

        private void initialize_EventMap()
        {
            EventMap map = new EventMap();
            map.eventStartCode = 114;  //Over Volt
            map.eventEndCode = 208;
            list_eventMap.Add(map);

            map = new EventMap();
            map.eventStartCode = 203; //Meter on Load
            map.eventEndCode = 204;
            list_eventMap.Add(map);

            map = new EventMap();
            map.eventStartCode = 201; //Over Load
            map.eventEndCode = 218;
            list_eventMap.Add(map);

            map = new EventMap();
            map.eventStartCode = 111; //Power Fail
            map.eventEndCode = 112;
            list_eventMap.Add(map);

            map = new EventMap();
            map.eventStartCode = 117; //Reverse Energy
            map.eventEndCode = 216;
            list_eventMap.Add(map);

            map = new EventMap();
            map.eventStartCode = 209; //Short Time Power Fail
            map.eventEndCode = 215;
            list_eventMap.Add(map);

            map = new EventMap();
            map.eventStartCode = 128; //Tamper Energy
            map.eventEndCode = 217;
            list_eventMap.Add(map);

            map = new EventMap();
            map.eventStartCode = 115; //Under Volt
            map.eventEndCode = 205;
            list_eventMap.Add(map);

            map = new EventMap();
            map.eventStartCode = 113; //Phase Fail
            map.eventEndCode = 221;
            list_eventMap.Add(map);

        }

        private bool isRecovery(int eventCode)
        {
            bool toreturn = false;

            switch (eventCode)
            {

                #region Meter on Load
                //    case 204:
                case 203:
                #endregion

                #region Over Load
                //    case 218:
                case 201:
                #endregion

                #region Over Volt
                //    case 208:
                case 114:
                #endregion

                #region Power Fail
                //    case 112:
                case 111:
                #endregion

                #region Reverse Energy
                //    case 216:
                case 117:
                #endregion

                #region Power Down
                //    case 215:
                case 209:
                #endregion

                #region Tamper Energy
                //    case 217:
                case 128:
                #endregion

                #region Under Volt
                //    case 205:
                case 115:
                #endregion

                #region Phase Fail
                case 113:
                    //case 221: 
                    #endregion
                    toreturn = true;
                    break;

            }

            return toreturn;
        }

        private void SaveAlarm(String File_URL, string meterInfo)  //3.3.7 QC
        {
            try
            {
                ///Save MajorAlarm Profile
                XMLParamsProcessor.Save_MajorAlarm(File_URL, meterInfo, Param_MajorAlarmProfile_obj);
                ///Save EventCautions
                XMLParamsProcessor.Save_EventsCautions(File_URL, ListEventCautions.ToArray());
            }
            catch (Exception ex)
            {
                Notification n = new Notification("Error Save_Major_Alarm", ex.Message);
            }
        }

        public void LoadAlarm(String File_URL = null, string currentMeterInfo = null)
        {
            List<Param_EventsCaution> _List_EventsCautions_toGET = null;
            Param_MajorAlarmProfile t_MajorAlarmProf = null;
            try
            {
                if (String.IsNullOrEmpty(File_URL) || String.IsNullOrWhiteSpace(File_URL))
                {
                    DirectoryInfo dictoryInfo = new DirectoryInfo(Environment.CurrentDirectory + "\\DLMS_saved_files\\");
                    File_URL = dictoryInfo.FullName;
                }
                if (String.IsNullOrEmpty(currentMeterInfo) || String.IsNullOrWhiteSpace(currentMeterInfo))
                {
                    currentMeterInfo = "R326";
                }
                t_MajorAlarmProf = XMLParamsProcessor.Load_MajorAlarm(File_URL, currentMeterInfo);
                ///Load EventCautions
                this.List_EventsCautions_toGET = XMLParamsProcessor.Load_EventsCautions(File_URL);
                _List_EventsCautions_toGET = new List<Param_EventsCaution>(List_EventsCautions_toGET);
                
            }
            catch (Exception ex)
            {
                Notification n = new Notification("Error Load_Major_Alarm", ex.Message);
            }
            finally
            {
                RefreshEventsConfiguration(t_MajorAlarmProf, _List_EventsCautions_toGET);
            }
        }

        private void btn_LoadAlarm_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo dictoryInfo = new DirectoryInfo(Environment.CurrentDirectory + "\\DLMS_saved_files\\");
                String File_URL = dictoryInfo.FullName;
                LoadAlarm(File_URL, ConnController.SelectedMeter.MeterModel);  //v3.3.7 QC
            }
            catch (Exception ex)
            {
                Notification n = new Notification("Error Load_Major_Alarm", ex.Message);
            }
        }

        private void btn_SaveAlarm_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo dictoryInfo = new DirectoryInfo(Environment.CurrentDirectory + "\\DLMS_saved_files\\");
                String File_URL = dictoryInfo.FullName;
                SaveAlarm(File_URL, application_Controller.ConnectionController.SelectedMeter.MeterModel); //3.3.7 QC
            }
            catch (Exception ex)
            {
                Notification n = new Notification("Error Save_Major_Alarm", ex.Message);
            }
        }

        private void Application_Controller_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                //Okay IsIOBusy Status
                if ("IsIOBusy".Equals(e.PropertyName) &&
                    sender is ApplicationController)
                {
                    UpdateReadWriteStatus(Application_Controller.IsIOBusy);
                }
            }
            catch
            {
            }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(ApplicationRight RightsArg)
        {
            bool isSuccess = false;
            try
            {
                this.SuspendLayout();
                this.grid_Events.SuspendLayout();

                //Initialize Class Level Variables
                RightsUser = RightsArg;
                MeterEventRights = RightsArg.MeterEventsRights;
                EventRights = RightsArg.EventsRights;

                var AccessRight = EventRights.Find((x) => x.QuantityType == typeof(SharedCode.Comm.HelperClasses.Events) && (x.Read || x.Write));

                if (AccessRight != null && (AccessRight.Read == true || AccessRight.Write == true))
                {
                    tb_Events.TabPages.Clear();
                    //Reset grid_Events
                    grid_Events.Columns.Clear();
                    grid_Events.Columns.AddRange(MajorAlarmGridColumnsAll.ToArray());
                    foreach (var item in EventRights)
                    {
                        _HelperAccessRights((SharedCode.Comm.HelperClasses.Events)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    }

                    //Make MajorAlarm + EventCautions Enable/Disable
                    AccessRight = EventRights.Find((x) => (x.QuantityName.Equals(SharedCode.Comm.HelperClasses.Events.MajorAlarm.ToString()) ||
                                                     x.QuantityName.Equals(SharedCode.Comm.HelperClasses.Events.EventCautions.ToString()))
                                                     && (x.Read || x.Write));

                    if (AccessRight != null && (AccessRight.Read || AccessRight.Write))
                    {
                        tb_Events.TabPages.Add(ProgramEvents);
                    }

                    try
                    {
                        //Update Event Item List
                        AddEventItemsToList();
                        //Update Program MajorAlarms
                        ShowtoGUI_EventCautions();
                        ShowtoGUI_MajorAlarmProfile();
                    }
                    catch
                    { }

                    isSuccess = true;
                }
                else
                    return false;
            }
            finally
            {
                ApplyRights_IsEventCompensationRequired();
                this.ResumeLayout();
                this.grid_Events.ResumeLayout();

                HidePrintReportButtons = false;
                if (RightsArg.GeneralRights.Find(x => x.QuantityName == GeneralRights.IgnoreReports.ToString() && x.Read == true) != null)
                {
                    HidePrintReportButtons = true;
                    this.btn_events_report.Visible = false;
                }
                else
                {
                    this.btn_events_report.Visible = true;
                }
            }
            return isSuccess;
        }

        private void _HelperAccessRights(SharedCode.Comm.HelperClasses.Events qty, bool read, bool write)
        {
            if (qty == SharedCode.Comm.HelperClasses.Events.LogBook)
            {
                if (read || write)
                {
                    tb_Events.TabPages.Add(tab_LogBook);
                }
            }
            else if (qty == SharedCode.Comm.HelperClasses.Events.IndividualEvents)
            {
                if (read || write)
                {
                    tb_Events.TabPages.Add(Individual_Events);
                }
            }
            else if (qty == SharedCode.Comm.HelperClasses.Events.MeterEventCounters)
            {
                if (read || write)
                {
                    tb_Events.TabPages.Add(Meter_Events);
                }
            }
            else if (qty == SharedCode.Comm.HelperClasses.Events.MajorAlarm)
            {
                if (!(read || write))
                {
                    foreach (DataGridViewColumn dt_GridCol in MajorAlarmGridColumns)
                    {
                        grid_Events.Columns.Remove(dt_GridCol);
                    }
                }
                btn_SetMajorAlarm.Visible = btn_SetMajorAlarm.Enabled = write;
                btn_SetMajorStatus.Visible = btn_SetMajorStatus.Enabled = write;
                btn_GetMajorAlarms.Visible = btn_GetMajorAlarms.Enabled = read;
            }
            else if (qty == SharedCode.Comm.HelperClasses.Events.EventCautions)
            {
                if (!(read || write))
                {
                    foreach (DataGridViewColumn dt_GridCol in CautionGridColumns)
                    {
                        grid_Events.Columns.Remove(dt_GridCol);
                    }
                }
                btn_SET_EventsCautions.Visible = btn_SET_EventsCautions.Enabled = write;
                btn_GET_EventsCautions.Visible = btn_GET_EventsCautions.Enabled = read;
            }
            else if (qty == SharedCode.Comm.HelperClasses.Events.SecurityData)
            {
                btn_ReadSecurityData.Visible = btn_ReadSecurityData.Enabled = read;
            }
        }

        //v4.8.23
        public void ApplyRights_IsEventCompensationRequired()
        {
            try
            {
                //IsWapdaFormat = true; //v4.8.31 true by default
                //IsWebFormat = false;
                IsEventTimeCompensationRequired = false;
                var _OtherRights = Application_Controller.CurrentUser.CurrentAccessRights.OtherRights;

                #region Event Time Compensation Rights
                try
                {
                    var right_IsEventCompensate = _OtherRights.Find((x) => x.QuantityName.Contains(OtherRights.IsMonitoringTimeCompensationInEvents.ToString()));
                    if (right_IsEventCompensate != null && (right_IsEventCompensate.Read)) IsEventTimeCompensationRequired = true;
                }
                catch (Exception) { }
                #endregion

                #region Web Format Report Rights
                //try
                //{
                //    var right_IsWebFormat = _OtherRights.Find((x) => x.QuantityName.Contains(OtherRights.IsWebFormat.ToString()));
                //    if (right_IsWebFormat != null && (right_IsWebFormat.Read)) IsWebFormat = true;
                //}
                //catch (Exception) { }
                #endregion
            }
            catch (Exception)
            {
            }

            #region Wapda Format Rights
            //try
            //{
            //    var _GenRights = Application_Controller.CurrentUser.CurrentAccessRights.GeneralRights;
            //    try
            //    {
            //        var right_IsWapdaFormat = _GenRights.Find((x) => x.QuantityName.Contains(GeneralRights.Events.ToString()));
            //        if (right_IsWapdaFormat != null && right_IsWapdaFormat.Write) IsWapdaFormat = false; //v4.8.31
            //    }
            //    catch (Exception) { }
            //}
            //catch (Exception) { }
            #endregion
            //if (IsWapdaFormat && IsWebFormat) IsWebFormat = false;  //v4.8.31 WapdaFormat important than Web Format
        }
        #endregion

    }
}

