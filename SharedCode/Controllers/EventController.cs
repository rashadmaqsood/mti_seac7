using comm.DataContainer;
using DatabaseConfiguration.DataSet;
using DLMS;
using DLMS.Comm;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.EventDispatcher.Contracts;
using SharedCode.Comm.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SharedCode.Controllers
{
    public class EventController
    {
        #region Data_Member

        internal int Max_Sync_Iteration = 50;
        public static readonly int Extra_Iteration = 25;
        protected ApplicationProcess_Controller _AP_Controller;
        private GenericProfileInfo EventLogGenericInfo = null;
        private EventInfo currentEventLog = null;

        private Configs configurations;
        private ConnectionInfo connInfo;
        private List<EventLogInfo> eventInfo;
        private Configurator _Configurator;

        private IEventDispatcher _majorAlarmDispatcher = null;
        private IEventPool _majorAlarmEventPool = null;
        private List<object> _dataToSave = null;

        #endregion

        #region Properties

        public ApplicationProcess_Controller AP_Controller
        {
            get { return _AP_Controller; }
            set { _AP_Controller = value; }
        }

        public object ParentContainer
        {
            get;
            set;
        }

        public Configs Configurations
        {
            get { return configurations; }
            set { configurations = value; }
        }

        public Configurator Configurator
        {
            get { return _Configurator; }
            set { _Configurator = value; }
        }

        public EventInfo CurrentEventLogInfo
        {
            get { return currentEventLog; }
            set { currentEventLog = value; }
        }

        public List<EventLogInfo> EventLogsInfo
        {
            get { return eventInfo; }
            set { eventInfo = value; }
        }

        public EventLogInfo CurrentEventLogInfoFromInfo
        {
            get
            {
                return GetEventLogInfo(this.CurrentEventLogInfo);
            }
        }

        public EventLogInfo GetEventLogInfo(EventInfo EventInfo)
        {
            try
            {
                // foreach (EventLogInfo item in this.EventLogInfoList)
                // {
                //     if (EventInfo.EventCode == item.EventCode)
                //     {
                //         return (EventLogInfo)item.Clone();
                //     }
                // }
                EventLogInfo item = this.EventLogInfoList.Find(x => x.EventCode.Equals(EventInfo.EventCode));
                if (item != null)
                    return item;
                else
                    throw new Exception(String.Format("Invalid EventInfo Object {0}", CurrentEventLogInfo));
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Invalid EventInfo Object {0}", CurrentEventLogInfo));
            }
        }

        public ConnectionInfo CurrentConnectionInfo
        {
            get { return connInfo; }
            set { connInfo = value; }
        }

        public List<object> RelatedDataToSave
        {
            get;
            // This Propert is shift to MDC applicationController
            //get
            //{
            //    try
            //    {
            //        if (_dataToSave == null ||
            //            _dataToSave.Count <= 0)
            //        {
            //            _dataToSave = new List<object>();
            //            MeterSerialNumber _msn = null;
            //            string CustomerRef = string.Empty;
            //            //Rashad long? customer_Id = null;
            //            string customer_Id = null;
            //            DateTime session_DateTime = DateTime.MinValue;

            //            if (ParentContainer != null &&
            //                ParentContainer is ApplicationController)
            //            {
            //                session_DateTime = ((ApplicationController)ParentContainer).SessionDateTime;

            //                if (((ApplicationController)ParentContainer).MeterInfo != null)
            //                {
            //                    //CustomerRef = ((ApplicationController)ParentContainer).MeterInfo.Reference_no;
            //                    //if (string.IsNullOrEmpty(CustomerRef))
            //                    //    CustomerRef = "00000000000000";
            //                    customer_Id = ((ApplicationController)ParentContainer).MeterInfo.Customer_ID;

            //                    string _msnLocal = ((ApplicationController)ParentContainer).MeterInfo.MSN;
            //                    uint msn = 0;

            //                    if (string.IsNullOrEmpty(_msnLocal) &&
            //                        UInt32.TryParse(_msnLocal, out msn))
            //                    {
            //                        _msn = new MeterSerialNumber() { MSN = msn };
            //                    }
            //                    else
            //                    {
            //                        IOConnection IOConnLocal = null;
            //                        if (((ApplicationController)ParentContainer).ConnectToMeter != null)
            //                        {
            //                            IOConnLocal = ((ApplicationController)ParentContainer).ConnectToMeter;
            //                        }

            //                        if (IOConnLocal != null &&
            //                           IOConnLocal.MeterSerialNumberObj != null &&
            //                            IOConnLocal.MeterSerialNumberObj.IsMSN_Valid)
            //                            _msn = IOConnLocal.MeterSerialNumberObj;
            //                    }
            //                }
            //            }

            //            _dataToSave.Add(_msn);
            //            _dataToSave.Add(session_DateTime);
            //            //_dataToSave.Add(CustomerRef);
            //            _dataToSave.Add(customer_Id);
            //        }
            //    }
            //    catch
            //    {
            //        _dataToSave = null;
            //    }

            //    return _dataToSave;
            //}
        }

        public IEventDispatcher MajorAlarmEventDispatcher
        {
            get { return _majorAlarmDispatcher; }
            set { _majorAlarmDispatcher = value; }
        }

        public IEventPool MajorAlarmEventPool
        {
            get { return _majorAlarmEventPool; }
            set { _majorAlarmEventPool = value; }
        }

        public bool RecvMajorAlarmEventNotification { get; set; }


        #endregion

        #region Constructor

        public EventController()
        {
            //Configurations = new Configs();
            EventLogGenericInfo = new GenericProfileInfo();
            RecvMajorAlarmEventNotification = true;
        }

        #endregion

        #region GET_Objects

        public Base_Class GetSAPEntry(Get_Index ObjIdentifier)
        {
            return AP_Controller.GetSAPEntry(ObjIdentifier);
        }

        public Base_Class GET_Object(Get_Index ObjIdentifier)
        {
            try
            {
                return GET_Object(ObjIdentifier, 0);
            }
            catch (Exception ex)               ///Other Reason Errors
            {
                throw ex;
            }
        }

        public Base_Class GET_Object(Get_Index ObjIdentifier, byte attribArg)
        {
            Base_Class objReceived = null;
            try
            {
                objReceived = AP_Controller.GET(ObjIdentifier, attribArg);
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
            {
            }
        }

        public Base_Class GET_Object(Base_Class ObjIdentifier, CancellationTokenSource Tk = null)
        {
            Base_Class objReceived = null;
            try
            {
                objReceived = AP_Controller.GET(ObjIdentifier, Tk);
                return objReceived;
            }
            catch (DLMSDecodingException ex)    ///Error Type 1 (Decoding Type Errors)
            {
                objReceived = ObjIdentifier;
                //return objReceived;
                throw ex;
            }
            catch (DLMSException ex)            ///Error Type 2 (DLMS Layer Errors)
            {
                objReceived = ObjIdentifier;
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
        /*
        public EntryDescripter MakeEntry(long Difference, long CurrentCounter, long OldCounter)
        {
            EntryDescripter event_entry = new EntryDescripter();

            if (Difference > Limits.Max_Events_Count_Limit && CurrentCounter > Limits.Max_Events_Count_Limit)
            {
                event_entry.FromEntry = 1;
                event_entry.ToEntry = Limits.Max_Events_Count_Limit;
            }
            else if (Difference <= Limits.Max_Events_Count_Limit && CurrentCounter > Limits.Max_Events_Count_Limit)
            {
                event_entry.FromEntry = (uint)(Limits.Max_Events_Count_Limit - Difference);
                event_entry.FromEntry++;
                event_entry.ToEntry = Limits.Max_Events_Count_Limit;
            }
            ///inital case when event counter < 100
            else if (CurrentCounter > OldCounter)
            {
                event_entry.FromEntry = Convert.ToUInt32(OldCounter + 1);
                event_entry.ToEntry = Convert.ToUInt32(CurrentCounter);
            }

            return event_entry;
        }
        */
        public EntryDescripter MakeEntry(long CurrentCounter, long OldCounter, uint MaxChunkSize = 100, uint Max_Events_Count_Limit = 100)
        {
            long Difference = CurrentCounter - OldCounter;
            Difference = Math.Abs(Difference);
            EntryDescripter event_entry = new EntryDescripter();

            if (Difference > Max_Events_Count_Limit && CurrentCounter > Max_Events_Count_Limit)
            {
                event_entry.FromEntry = 1;
                event_entry.ToEntry = 0;
            }
            else if (Difference <= Max_Events_Count_Limit && CurrentCounter > Max_Events_Count_Limit)
            {
                event_entry.FromEntry = (uint)(Max_Events_Count_Limit - Difference);
                event_entry.ToEntry = (uint)((Difference < MaxChunkSize) ? Max_Events_Count_Limit : event_entry.FromEntry + MaxChunkSize);
                event_entry.FromEntry++;
            }
            ///inital case when event counter < 100
            else if (CurrentCounter > OldCounter && CurrentCounter < Max_Events_Count_Limit)
            {
                event_entry.FromEntry = Convert.ToUInt32(OldCounter);
                event_entry.ToEntry = (uint)((Difference < MaxChunkSize) ? CurrentCounter : event_entry.FromEntry + MaxChunkSize);
                event_entry.FromEntry++;
            }
            return event_entry;
        }


        #endregion

        #region Support_Methods_&_Properties

        public List<EventInfo> EventInfoList
        {
            get
            {
                try
                {
                    List<EventInfo> temp = null;
                    try
                    {
                        if (temp == null || temp.Count <= 0)
                        {
                            temp = Configurator.GetMeterEventInfo(CurrentConnectionInfo);
                        }
                    }
                    catch (Exception) { }
                    finally
                    {
                        if (temp == null || temp.Count <= 0)
                        {
                            temp = GetEventInfo();
                        }
                    }
                    temp.Sort((x, y) => x.EventCode.CompareTo(y.EventCode));
                    return temp;
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to load events info from configurations", ex);
                }
            }
        }

        public List<EventLogInfo> EventLogInfoList
        {
            get
            {
                try
                {
                    //ConfigsHelper Configurater = new ConfigsHelper(Configurations);
                    //List<EventLogInfo> temp = configurater.GetEventLogInfoItems();
                    List<EventLogInfo> temp = eventInfo;
                    try
                    {
                        if (temp == null || temp.Count <= 0)
                        {
                            temp = Configurator.GetMeterEventLogInfo(CurrentConnectionInfo);
                            temp.Sort((x, y) => x.EventLogIndex.CompareTo(y.EventLogIndex));
                            eventInfo = temp;
                        }
                    }
                    catch { }
                    finally
                    {
                        if (temp == null || temp.Count <= 0)
                        {
                            temp = GetEventLogInfo();
                            temp.Sort((x, y) => x.EventLogIndex.CompareTo(y.EventLogIndex));
                        }
                    }
                    //eventInfo = temp;
                    return temp;
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to load events log info from configurations", ex);
                }
            }
        }

        public IAccessSelector ComputeEventAccessSelector(EventLogInfo EventLogInfoObj, GenericProfileInfo eventGenericInfo)
        {
            try
            {
                IAccessSelector selector = null;
                if (eventGenericInfo.ReadByDateTime)
                {
                    RangeDescripter rSelector = new RangeDescripter();
                    rSelector.EncodingDataType = DataTypes._A19_datetime;
                    rSelector.FromEntry = eventGenericInfo.FromDate;
                    rSelector.ToEntry = eventGenericInfo.ToDate;
                    selector = rSelector;
                }
                else
                {
                    if (EventLogInfoObj == null)
                        return null;
                    else if (EventLogInfoObj.PreviousEventCounter == EventLogInfo.EventCounterNotApplied)
                        return null;
                    else if (EventLogInfoObj.MaxEventCount == EventLogInfo.MaxEventCountNotApplied)
                        return null;
                    else if (EventLogInfoObj.MaxEventCount < eventGenericInfo.EntriesInUse)
                    {
                        // Temporary Code Modified
                        eventGenericInfo.EntriesInUse = (uint)EventLogInfoObj.MaxEventCount;
                    }

                    EntryDescripter eSelecter = new EntryDescripter();
                    long DeltaEventCount = (EventLogInfoObj.CurrentEventCounter - EventLogInfoObj.PreviousEventCounter);

                    // Very First Request
                    if (DeltaEventCount == 0 && EventLogInfoObj.PreviousEventCounter == 0)
                        return null;

                    // Data Upto date,don't request
                    if (DeltaEventCount == 0)
                    {
                        return null;
                        // throw new Exception("Unable to compute Entry Access Selecter for Load Profile Data,records are updated");
                    }

                    switch (eventGenericInfo.SortMethod)
                    {
                        case SortMethod.FIFO:
                            {
                                eSelecter.ToEntry = EntryDescripter.MaxPossibleValue;
                                long StartIndex = eventGenericInfo.EntriesInUse - DeltaEventCount;
                                if (StartIndex <= 0)
                                {
                                    eSelecter.FromEntry = 1;
                                }
                                else
                                    eSelecter.FromEntry = (uint)StartIndex + 1;
                                break;
                            }
                        case SortMethod.LIFO:
                            {
                                eSelecter.FromEntry = 1;
                                long LastIndex = eSelecter.FromEntry + DeltaEventCount;
                                if (LastIndex > eventGenericInfo.EntriesInUse)
                                {
                                    eSelecter.ToEntry = eventGenericInfo.EntriesInUse;
                                }
                                else
                                    eSelecter.ToEntry = (uint)(LastIndex - 1);
                                break;
                            }
                        default:
                            throw new Exception(String.Format("Sort Method is {0} not exptected", eventGenericInfo.SortMethod));
                            break;
                    }
                    selector = eSelecter;
                }
                return selector;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to compute Entry Access Selecter for event logs");
            }
            return null;
        }

        #endregion

        #region Get_Event_Counter

        public uint Get_EventCounter_Internal()
        {
            try
            {
                Class_1 Event_Log_Counter = (Class_1)GET_Object((Get_Index.Event_Counter_00), 0x02);
                if (Event_Log_Counter.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {

                    return Convert.ToUInt32(Event_Log_Counter.Value);

                }
                else
                    throw new Exception(String.Format("Error getting Event log {0} internal counter", CurrentEventLogInfo));
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error getting Event log {0} internal counter", CurrentEventLogInfo), ex);
            }
        }

        //public uint Get_EventCounter_Internal()
        //{
        //    try
        //    {
        //        Class_1 Event_Log_Counter = (Class_1)GET_Object((Get_Index)(CurrentEventLogInfoFromInfo.EventCounterIndex), 0x02);
        //        if (Event_Log_Counter.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
        //        {
        //            return Convert.ToUInt32(Event_Log_Counter.Value);
        //        }
        //        else
        //            throw new Exception(String.Format("Error getting Event log {0} internal counter", CurrentEventLogInfo));
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(String.Format("Error getting Event log {0} internal counter", CurrentEventLogInfo), ex);
        //    }
        //}

        public uint Get_EventCounter_Internal(EventInfo EventInfoObj)
        {
            try
            {
                EventLogInfo EventLogInfoObj = null;
                if (EventInfoObj is EventLogInfo)
                    EventLogInfoObj = (EventLogInfo)EventInfoObj;
                else
                    EventLogInfoObj = GetEventLogInfo(EventInfoObj);
                Class_1 Event_Log_Counter = (Class_1)GET_Object((Get_Index)(EventLogInfoObj.EventCounterIndex), 0x02);
                if (Event_Log_Counter.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {
                    return Convert.ToUInt32(Event_Log_Counter.Value);
                }
                else
                    throw new Exception(String.Format("Error getting Event log {0} internal counter", EventInfoObj));
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error getting Event log {0} internal counter", EventInfoObj), ex);
            }
        }

        public List<EventItem> Get_EventLog_Counters_Internal(List<EventInfo> EventInfoList)
        {
            List<EventItem> EventCountersList = new List<EventItem>();
            foreach (EventInfo item in EventInfoList)
            {
                uint Counter = Get_EventCounter_Internal(item);
                EventItem NewEventCOunter = new EventItem();
                NewEventCOunter.EventCounter = Counter;
                NewEventCOunter.EventInfo = (EventInfo)item.Clone();
                EventCountersList.Add(NewEventCOunter);
            }
            return EventCountersList;
        }

        public List<EventItem> Get_EventLog_Counters_Internal()
        {
            return Get_EventLog_Counters_Internal(this.EventInfoList);
        }

        #endregion

        #region GenericProfileInfo

        public GenericProfileInfo GetEventLogGenericInfo(EventLogInfo CurrentEventLogInfoObj)
        {
            try
            {
                GenericProfileInfo EventInfo = new GenericProfileInfo();
                Class_7 EventLog_CommObj = (Class_7)GetSAPEntry((Get_Index)CurrentEventLogInfoObj.EventLogIndex);
                #region Init_Event_Capture_List

                try
                {
                    StOBISCode ProfileOBISCode = EventLog_CommObj.INDEX;
                    EventLog_CommObj.captureObjectsList = Configurator.GetProfileCaptureObjectList(CurrentConnectionInfo, ProfileOBISCode);
                    EventLog_CommObj.BaseClassCaptureObjectsList = EventLog_CommObj.InitializeCaptureBuffer(1);
                }
                catch (Exception)
                {
                    EventLog_CommObj.captureObjectsList = new List<CaptureObject>();
                }

                #endregion

                if (EventLog_CommObj.IsAttribReadable(0x03) &&
                    !EventLog_CommObj.IsCaptureObjectListInit)
                {
                    EventLog_CommObj.DecodingAttribute = 0x03;
                    // Request Capture Object List
                    EventLog_CommObj = (Class_7)GET_Object(EventLog_CommObj);
                    #region // Save Event Logs Capture Buffer Info
                    #region Debugging & Logging

#if Enable_DEBUG_RunMode
                    
                    try
                    {
                        StOBISCode OBIS_Index_PowerProfile = EventLog_CommObj.INDEX;
                        Configurator.SaveProfileCaptureObjectList(CurrentConnectionInfo, EventLog_CommObj.captureObjectsList, OBIS_Index_PowerProfile, null);
                    }
                    catch(Exception ex)
                    {
                        Commons.WriteError(String.Format("Error occurred while saving Event Logs {0} Capture List Info,Error Details {1}"
                                                        , (Get_Index)CurrentEventLogInfoObj.EventLogIndex, ex.Message));
                    }

#endif
                    #endregion
                    #endregion
                }
                else if (!EventLog_CommObj.IsAttribReadable(0x03))
                    throw new Exception(String.Format("Error occurred reading {0} Event Info,No Reading Access Rights,unable to read Capture ObjectInfo",
                                                       CurrentEventLogInfoObj));

                if (EventLog_CommObj.IsCaptureObjectListInit)
                {
                    EventLog_CommObj.BaseClassCaptureObjectsList = EventLog_CommObj.InitializeCaptureBuffer(1);
                    EventInfo.CaptureBufferInfo.AddRange(EventLog_CommObj.BaseClassCaptureObjectsList);
                }
                else
                    throw new Exception(String.Format("Error occurred reading {0} Event Info,Capture ObjectInfo not initialized ", CurrentEventLogInfoObj));

                return EventInfo;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred reading {0} Event Log Info data", CurrentEventLogInfoObj), ex);
            }
        }

        public GenericProfileInfo GetEventLogGenericInfo()
        {
            try
            {
                EventLogInfo EventLogInfoObj = this.CurrentEventLogInfoFromInfo;
                return GetEventLogGenericInfo(EventLogInfoObj);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred reading {0} Event Log Info data", CurrentEventLogInfo), ex);
            }
        }

        #endregion

        #region Read_Event_Log_Data

        public bool TryReadEventLogDataRaw(IAccessSelector AccessSelector,
            EventLogInfo EventLogInfoObj, List<ILValue[]> eventRecords,
            Action<Exception> SetInnerException = null, CancellationTokenSource TK = null)
        {
            Exception InternalExeception = null;
            bool isSuccess = false;
            try
            {
                Class_7 EventLogObj = (Class_7)GetSAPEntry((Get_Index)EventLogInfoObj.EventLogIndex);
                // EventLogObj.AccessSelector = null;
                if (EventLogObj != null && !EventLogObj.IsCaptureObjectListIntialized)
                {
                    ///Init Event Log Object
                    GetEventLogGenericInfo(EventLogInfoObj);
                }
                if (EventLogObj.IsAccessSelecterApplied(02) == SelectiveAccessType.Entry_Descripter ||
                    EventLogObj.IsAccessSelecterApplied(02) == SelectiveAccessType.Both_Types)
                {
                    EventLogObj.AccessSelector = AccessSelector;
                }
                else
                    EventLogObj.AccessSelector = null;
                EventLogObj.DecodingAttribute = 0x02;
                try
                {
                    EventLogObj = (Class_7)(GET_Object(EventLogObj, TK));
                }
                catch (Exception ex)
                {
                    InternalExeception = ex;
                    isSuccess = false;
                }
                DecodingResult d = EventLogObj.GetAttributeDecodingResult(0x02);
                if (d == DecodingResult.Ready)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                    if (InternalExeception != null)
                        InternalExeception = new Exception("Decoding result not ready", InternalExeception);
                }
                if (EventLogObj.buffer != null && EventLogObj.buffer.Count > 0)
                {
                    IDataGenerator EventLogDataGenerator = new EventDataGenerator();
                    List<ILValue[]> eventRecords_T = EventLogDataGenerator.GetData(((Class_7)EventLogObj).buffer);
                    eventRecords.AddRange(eventRecords_T);
                }
            }
            catch (ThreadAbortException ex)
            {
                isSuccess = false;
                InternalExeception = ex;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                InternalExeception = new Exception(String.Format("Error occurred while reading event {0} records data", EventLogInfoObj), ex);
            }
            finally
            {
                if (InternalExeception != null)
                    throw InternalExeception;
            }
            return isSuccess;
        }

        public bool TryReadEventLogDataInChunks(Profile_Counter Event_Counters, EventInfo EventLogInfoObj, EventData EventInstances,
            Action<Exception> SetInnerException = null, uint maxChunkSize = 25, CancellationTokenSource CancelToken = null)
        {
            Exception InnerException = null;
            bool IsSuccess = false;
            List<ILValue[]> EventRawData = new List<ILValue[]>();
            long I_diff_Counter = 0, IL_Counter = 0, IH_Counter = 0, start_Counter = 0;
            uint ct_minCount = 0;
            uint ct_maxCount = 0;
            uint ct_MaxCount_Main = 0;
            EventData tempEventData = new EventData();
            try
            {
                EventLogInfo infoObj = null;
                if (EventLogInfoObj is EventLogInfo)
                    infoObj = (EventLogInfo)EventLogInfoObj;
                else
                    infoObj = GetEventLogInfo(EventLogInfoObj);
                EventInstances.EventInfo = infoObj;
                tempEventData.EventInfo = infoObj;
                //Update Current Event Counter
                if (Event_Counters.Meter_Counter <= 0)
                {
                    Event_Counters.Meter_Counter = Get_EventCounter_Internal();
                }
                long difference = Event_Counters.Meter_Counter - Event_Counters.DB_Counter;
                if (difference == 0 && Event_Counters.Meter_Counter > 0)
                {
                    IsSuccess = true;
                    return IsSuccess;
                }
                else if (difference < 0) difference *= -1;
                //Init Inter- Event Counters
                start_Counter = (Event_Counters.Meter_Counter > Event_Counters.Max_Size) ?
                    (Event_Counters.Meter_Counter - Event_Counters.Max_Size) : 0;
                IL_Counter = (start_Counter <= Event_Counters.DB_Counter) ? Event_Counters.DB_Counter : start_Counter;
                IH_Counter = (difference <= maxChunkSize) ? Event_Counters.Meter_Counter : (IL_Counter + maxChunkSize - 1);
                //Init Max Iteration Counters
                int Iteration_Counter = 0;
                Max_Sync_Iteration = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(difference / maxChunkSize))) + Extra_Iteration;

                while (IL_Counter < Event_Counters.Meter_Counter &&
                    IL_Counter >= Event_Counters.DB_Counter &&
                    IL_Counter != IH_Counter &&
                    Iteration_Counter < Max_Sync_Iteration)
                {
                    try
                    {
                        #region Compute Entry Descriptor

                        I_diff_Counter = IH_Counter - IL_Counter;
                        EntryDescripter event_entry = MakeEntry(Event_Counters.Meter_Counter, IL_Counter, maxChunkSize, Event_Counters.Max_Size);

                        #endregion
                        EventRawData.Clear();
                        ///Read Internal Events Data In Chunks
                        IsSuccess = TryReadEventLogDataRaw(event_entry, infoObj, EventRawData, (ex) => InnerException = ex);

                        // Format Event Data Object
                        Func<Get_Index, StOBISCode> dlg = null;

                        if (_AP_Controller != null && _AP_Controller.ApplicationProcessSAPTable != null)
                            dlg = new Func<Get_Index, StOBISCode>(_AP_Controller.GetOBISCode);

                        IEventDataFormatter EventDataFormater = new EventDataFormatter(this.EventLogInfoList) { OBISLabelLookup = dlg };

                        if (EventRawData.Count > 0)
                        {
                            tempEventData = EventDataFormater.MakeData(EventRawData);
                            tempEventData.EventInfo = infoObj;
                        }
                        #region Raise Internal Error

                        if (InnerException != null)
                            throw InnerException;
                        else if (!IsSuccess)
                            throw new Exception("Error occurred while reading Event Data");

                        #endregion
                    }
                    finally
                    {
                        if (tempEventData.EventRecords != null &&
                            tempEventData.EventRecords.Count > 0)
                        {
                            ///Update In Main Data
                            EventInstances.EventRecords.AddRange(tempEventData.EventRecords);
                            ct_minCount = tempEventData.MinEventCounter;
                            ct_maxCount = tempEventData.MaxEventCounter;
                            ct_MaxCount_Main = EventInstances.MaxEventCounter;
                            tempEventData.EventRecords.Clear();
                        }

                        #region Perform Counters Analysis

                        //Event Counters Missing re-Request Old Data
                        if ((IL_Counter + 1) < ct_minCount && ct_minCount > (start_Counter + 1))
                        {
                            long diff_recv = Math.Abs(ct_minCount - IL_Counter);
                            //Update Current Counter,Start_Counters Here
                            if (diff_recv > maxChunkSize)
                            {
                                Event_Counters.Meter_Counter = Get_EventCounter_Internal();
                            }
                            else
                            {
                                //Update Here Current_Counter etc
                                Event_Counters.Meter_Counter = (uint)(Event_Counters.Meter_Counter + diff_recv);
                            }
                            //Init Inter- Event Counters
                            start_Counter = (Event_Counters.Meter_Counter > Event_Counters.Max_Size) ?
                                (Event_Counters.Meter_Counter - Event_Counters.Max_Size) : 0;
                            IL_Counter = (start_Counter >= IL_Counter) ? start_Counter : IL_Counter;
                            IH_Counter = (diff_recv <= maxChunkSize) ? ct_minCount : (IL_Counter + maxChunkSize - 1);
                            ///Update Next Data Request
                            if (IL_Counter >= ct_minCount)
                            {
                                //Init Inter-Event Counters
                                IL_Counter = (ct_MaxCount_Main > ct_maxCount) ? ct_MaxCount_Main : ct_maxCount;
                                long newCounter = IL_Counter + maxChunkSize - 1;
                                IH_Counter = (newCounter > Event_Counters.Meter_Counter) ? Event_Counters.Meter_Counter : (newCounter);
                            }
                        }
                        else if (ct_maxCount < IH_Counter)
                        {
                            //Init Inter-Event Counters
                            IL_Counter = ct_maxCount;
                        }
                        //Counters As Expected
                        //Compute Next Chunk IL_Counter,IH_Coutner 
                        else
                        {
                            //Init Inter-Event Counters
                            IL_Counter = (ct_MaxCount_Main > ct_maxCount) ? ct_MaxCount_Main : ct_maxCount;
                            long newCounter = IL_Counter + maxChunkSize - 1;
                            IH_Counter = (newCounter > Event_Counters.Meter_Counter) ? Event_Counters.Meter_Counter : (newCounter);
                        }

                        #endregion
                        Iteration_Counter++;
                        if (CancelToken.IsCancellationRequested)
                        {
                            IsSuccess = false;
                            CancelToken.Token.ThrowIfCancellationRequested();
                        }
                    }
                }
                return IsSuccess;
            }
            catch (TaskCanceledException ex)
            {
                InnerException = ex;
            }

            catch (ThreadAbortException ex)
            {
                InnerException = ex;
            }
            catch (Exception ex)
            {
                InnerException = new Exception(String.Format("Error occurred while reading events {0} records data (Error Code:{1})", EventLogInfoObj, (int)MDCErrors.App_Events_Data_Read), ex);
            }
            finally
            {
                EventInstances.MaxCounterRead = ct_MaxCount_Main;
                #region  ///Remove Duplicate Event Counters
                if (EventInstances != null && EventInstances.EventRecords != null && EventInstances.EventRecords.Count > 0)
                {

                    for (uint EventCounter = Event_Counters.DB_Counter; EventCounter < Event_Counters.Meter_Counter; EventCounter++)
                    {
                        List<EventItem> t = EventInstances.EventRecords.FindAll((x) => x != null && x.EventCounter == EventCounter);
                        if (t.Count > 1)
                        {
                            t.RemoveAt(0);
                            foreach (var item in t)
                            {
                                EventInstances.EventRecords.Remove(item);
                            }
                        }
                    }
                }
                #endregion
                if (SetInnerException != null && InnerException != null)
                    SetInnerException(InnerException);
            }
            return IsSuccess;
        }

        public virtual bool TryReadEventLogData(Profile_Counter Event_Counters, EventInfo EventLogInfoObj, EventData EventInstances,
            Action<Exception> SetInnerException = null, CancellationTokenSource CancelToken = null)
        {
            Exception InnerException = null;
            bool IsSuccess = false;
            List<ILValue[]> EventRawData = new List<ILValue[]>();
            uint ct_minCount = 0;
            uint ct_maxCount = 0;
            uint ct_MaxCount_Main = 0;
            EventData tempEventData = new EventData();
            try
            {
                EventLogInfo infoObj = null;
                if (EventLogInfoObj is EventLogInfo)
                    infoObj = (EventLogInfo)EventLogInfoObj;
                else
                    infoObj = GetEventLogInfo(EventLogInfoObj);

                EventInstances.EventInfo = infoObj;
                tempEventData.EventInfo = infoObj;

                //Update Current Event Counter
                if (Event_Counters.Meter_Counter <= 0)
                    Event_Counters.Meter_Counter = Get_EventCounter_Internal();

                long difference = Event_Counters.Meter_Counter - Event_Counters.DB_Counter;
                if (difference == 0 && Event_Counters.Meter_Counter > 0)
                {
                    IsSuccess = true;
                    return IsSuccess;
                }
                else if (difference < 0) difference *= -1;

                try
                {
                    EventRawData.Clear();
                    //Read Internal Events Data In Chunks
                    IsSuccess = TryReadEventLogDataRaw(null, infoObj, EventRawData, (ex) => InnerException = ex, CancelToken);

                    // Format Event Data Object
                    Func<Get_Index, StOBISCode> dlg = null;

                    if (_AP_Controller != null && _AP_Controller.ApplicationProcessSAPTable != null)
                        dlg = new Func<Get_Index, StOBISCode>(_AP_Controller.GetOBISCode);

                    IEventDataFormatter EventDataFormater = new EventDataFormatter(this.EventLogInfoList) { OBISLabelLookup = dlg };

                    if (EventRawData.Count > 0)
                    {
                        tempEventData = EventDataFormater.MakeData(EventRawData);
                        tempEventData.EventInfo = infoObj;
                    }

                    #region Raise Internal Error

                    if (InnerException != null)
                        throw InnerException;
                    else if (!IsSuccess)
                        throw new Exception("Error occurred while reading Event Data");

                    #endregion
                }
                finally
                {
                    if (tempEventData.EventRecords != null &&
                        tempEventData.EventRecords.Count > 0)
                    {
                        //Update In Main Data
                        EventInstances.EventRecords.AddRange(tempEventData.EventRecords);
                        ct_minCount = tempEventData.MinEventCounter;
                        ct_maxCount = tempEventData.MaxEventCounter;
                        ct_MaxCount_Main = EventInstances.MaxEventCounter;
                        tempEventData.EventRecords.Clear();
                    }
                    if (CancelToken.IsCancellationRequested)
                    {
                        IsSuccess = false;
                        CancelToken.Token.ThrowIfCancellationRequested();
                    }
                }
                return IsSuccess;
            }
            catch (TaskCanceledException ex)
            {
                InnerException = ex;
            }
            catch (ThreadAbortException ex)
            {
                InnerException = ex;
            }
            catch (Exception ex)
            {
                InnerException = new Exception(String.Format("Error occurred while reading events {0} records data", EventLogInfoObj), ex);
            }
            finally
            {
                EventInstances.MaxCounterRead = ct_MaxCount_Main;
                #region  // Remove Duplicate Event Counters

                if (EventInstances != null && EventInstances.EventRecords != null && EventInstances.EventRecords.Count > 0)
                {

                    for (uint EventCounter = Event_Counters.DB_Counter; EventCounter < Event_Counters.Meter_Counter; EventCounter++)
                    {
                        List<EventItem> t = EventInstances.EventRecords.FindAll((x) => x != null && x.EventCounter == EventCounter);
                        if (t.Count > 1)
                        {
                            t.RemoveAt(0);
                            foreach (var item in t)
                            {
                                EventInstances.EventRecords.Remove(item);
                            }
                        }
                    }
                }

                #endregion
                if (SetInnerException != null && InnerException != null)
                    SetInnerException(InnerException);
            }
            return IsSuccess;
        }

        public List<ILValue[]> ReadEventLogDataRaw(IAccessSelector AccessSelector, EventLogInfo EventLogInfoObj)
        {
            try
            {
                Class_7 EventLogObj = (Class_7)GetSAPEntry((Get_Index)EventLogInfoObj.EventLogIndex);
                // EventLogObj.AccessSelector = null;
                if (EventLogObj != null && !EventLogObj.IsCaptureObjectListIntialized)
                {
                    ///Init Event Log Object
                    GetEventLogGenericInfo(EventLogInfoObj);
                }
                if (EventLogObj.IsAccessSelecterApplied(02) == SelectiveAccessType.Entry_Descripter ||
                    EventLogObj.IsAccessSelecterApplied(02) == SelectiveAccessType.Both_Types)
                {
                    EventLogObj.AccessSelector = AccessSelector;
                }
                else
                    EventLogObj.AccessSelector = null;
                EventLogObj.DecodingAttribute = 0x02;
                EventLogObj = (Class_7)GET_Object(EventLogObj);
                if (EventLogObj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                {
                    IDataGenerator EventLogDataGenerator = new EventDataGenerator();
                    List<ILValue[]> eventRecords = EventLogDataGenerator.GetData(((Class_7)EventLogObj).buffer);
                    return eventRecords;
                }
                else
                    // throw new Exception(String.Format("Error occurred while reading event {0} records data", EventLogInfoObj));
                    throw new Exception("Decoding result not ready");
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while reading event {0} records data", EventLogInfoObj), ex);
            }
        }

        public EventData ReadEventLogData(EventInfo EventLogInfoObj, DateTime fromDate, DateTime toDate)
        {
            EventLogInfo LogObj = null;
            if (EventLogInfoObj is EventLogInfo)
                LogObj = (EventLogInfo)EventLogInfoObj;
            else
                LogObj = GetEventLogInfo(EventLogInfoObj);
            GenericProfileInfo genericEventLogInfo = new GenericProfileInfo()
            {
                ReadByDateTime = true,
                FromDate = fromDate,
                ToDate = toDate

            };
            IAccessSelector selector = ComputeEventAccessSelector(LogObj, genericEventLogInfo);
            return ReadEventLogData(selector, LogObj);
        }

        public EventData ReadEventLogData(IAccessSelector AccessSelector, EventInfo EventLogInfoObj)
        {
            try
            {
                List<ILValue[]> EventRawData;
                EventLogInfo infoObj = null;
                if (EventLogInfoObj is EventLogInfo)
                    infoObj = (EventLogInfo)EventLogInfoObj;
                else
                    infoObj = GetEventLogInfo(EventLogInfoObj);
                //if (infoObj.EventName == "Combine Event Log")

                EventRawData = ReadEventLogDataRaw(AccessSelector, infoObj);
                ///Format Event Data Object
                //TODO:Modification For Events
                IList<EventInfo> listEvLogInfo = new List<EventInfo>(this.EventLogInfoList);

                // Format Event Data Object
                Func<Get_Index, StOBISCode> dlg = null;

                if (_AP_Controller != null && _AP_Controller.ApplicationProcessSAPTable != null)
                    dlg = new Func<Get_Index, StOBISCode>(_AP_Controller.GetOBISCode);

                IEventDataFormatter EventDataFormater = new EventDataFormatter(listEvLogInfo) { OBISLabelLookup = dlg };

                EventData EventInstances = new EventData();

                if (EventRawData.Count > 0)
                    EventInstances = EventDataFormater.MakeData(EventRawData);
                EventInstances.EventInfo = (EventInfo)EventLogInfoObj.Clone();
                return EventInstances;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while reading event {0} records data", EventLogInfoObj), ex);
            }
        }

        public List<EventData> ReadEventLogData(IAccessSelector AccessSelector, List<EventInfo> EventLogInfoObjList)
        {
            try
            {
                List<EventData> EventsLogs = new List<EventData>();
                foreach (var eventLog in EventLogInfoObjList)
                {
                    EventData eventLogObj = ReadEventLogData(AccessSelector, eventLog);
                    EventsLogs.Add(eventLogObj);
                }
                return EventsLogs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EventData ReadEventLogData(EventInfo EventLogInfoObj, CancellationTokenSource TK = null)
        {
            try
            {
                EventLogInfo LogObj = null;
                EventData Event_Data = new EventData();
                if (EventLogInfoObj is EventLogInfo)
                    LogObj = (EventLogInfo)EventLogInfoObj;
                else
                    LogObj = GetEventLogInfo(EventLogInfoObj);

                // Init Event_Counter
                Profile_Counter Event_Counter = new Profile_Counter()
                {
                    Meter_Counter = (uint)LogObj.CurrentEventCounter,
                    DB_Counter = (uint)LogObj.PreviousEventCounter,
                    Max_Size = (uint)LogObj.MaxEventCount
                };

                // Read Event Log Info Details
                Event_Counter.Meter_Counter = (uint)(LogObj.CurrentEventCounter = Get_EventCounter_Internal(LogObj));
                // Try To Normalize Previous Event Counter
                GenericProfileInfo genericEventLogInfo = GetEventLogGenericInfo(LogObj);

                Exception innerException = null;
                bool isSuccessful = false;

                #region  DUMMY Checking Code

                //List<EventInfo> eventsToRead = new List<EventInfo>();

                //EventLogInfo ei = new EventLogInfo();
                //    ei.CurrentEventCounter = -9223372036854775808;
                //    ei.EventCode	        =    105;
                //    ei.EventCounterIndex = DLMS.Get_Index.Event_Counter_28;
                //    ei.EventId = MeterEvent.TimeChange;
                //    ei.EventLogIndex = Get_Index._Event_Log_28;
                //    ei.EventName	= "TimeChange";
                //    ei.MaxEventCount	=50;
                //    ei.PreviousEventCounter= -9223372036854775808;
                //    ei._EventId=	28;

                //eventsToRead.Add(ei);

                //List<EventInfo> eventsIndividualRead = new List<EventInfo>();
                //List<EventData> _obj_EventData = new List<EventData>();
                ////_obj_EventData = _EventController.ReadEventLogData(eventsToRead);
                //isSuccessful = this.TryReadEventLogData(eventsToRead, ref _obj_EventData, (ex) => innerException = ex, null);
                ////Update Individual Events Read Successfully
                //foreach (var ev_Data in _obj_EventData)
                //{
                //    if (ev_Data != null && ev_Data.EventInfo != null)
                //        eventsIndividualRead.Add(ev_Data.EventInfo);
                //}

                #endregion



                isSuccessful = TryReadEventLogData(Event_Counter, EventLogInfoObj, Event_Data, (ex) => innerException = ex, TK);
                if (!isSuccessful && innerException != null)
                    throw innerException;
                else if (!isSuccessful && innerException == null)
                {
                    innerException = new Exception(String.Format("Error occurred while reading Event Data {0}", LogObj));
                    throw innerException;
                }

                return Event_Data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EventData> ReadEventLogData(List<EventInfo> EventLogInfoObjList, CancellationTokenSource TK = null)
        {
            try
            {
                List<EventData> EventsLogs = new List<EventData>();
                foreach (var eventLog in EventLogInfoObjList)
                {
                    EventData eventLogObj = ReadEventLogData(eventLog, TK);
                    EventsLogs.Add(eventLogObj);
                }
                return EventsLogs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool TryReadEventLogData(List<EventInfo> EventLogInfoObjList, ref List<EventData> EventData, Action<Exception> SetInnerException = null, CancellationTokenSource TK = null)
        {
            bool isSuccess = false;
            if (EventData == null)
                EventData = new List<EventData>();
            EventData.Clear();
            // Init locals
            EventLogInfo LogObj = null;
            EventData Event_Data = new EventData();
            // Init Event_Counter
            Profile_Counter Event_Counter = null;
            Exception innerException = null;
            bool isSuccessful = false;

            try
            {

                foreach (var eventLog in EventLogInfoObjList)
                {
                    if (TK != null)
                        TK.Token.ThrowIfCancellationRequested();
                    Event_Data = new EventData();

                    if (eventLog is EventLogInfo)
                        LogObj = (EventLogInfo)eventLog;
                    else
                        LogObj = GetEventLogInfo(eventLog);

                    // Init Event_Counter
                    Event_Counter = new Profile_Counter()
                    {
                        Meter_Counter = (uint)LogObj.CurrentEventCounter,
                        DB_Counter = (uint)LogObj.PreviousEventCounter,
                        Max_Size = (uint)LogObj.MaxEventCount
                    };

                    // Read Event Log Info Details
                    Event_Counter.Meter_Counter = (uint)(LogObj.CurrentEventCounter = Get_EventCounter_Internal(LogObj));
                    // Try To Normalize Previous Event Counter
                    GenericProfileInfo genericEventLogInfo = GetEventLogGenericInfo(LogObj);

                    innerException = null;
                    isSuccessful = false;

                    isSuccessful = TryReadEventLogData(Event_Counter, LogObj, Event_Data, (ex) => innerException = ex, TK);

                    if (isSuccessful)
                    {
                        EventData.Add(Event_Data);
                        continue;
                    }
                    else if (!isSuccessful && innerException != null)
                        throw innerException;
                    else if (!isSuccessful && innerException == null)
                    {
                        innerException = new Exception(String.Format("Error occurred while reading Event Data {0} (Error Code:{1})", LogObj, (int)MDCErrors.App_Individual_Events_Data_Read));
                        throw innerException;
                    }
                }
                isSuccess = true;
                return isSuccess;
            }
            catch (Exception ex)
            {
                if (SetInnerException != null)
                    SetInnerException(ex);
            }

            return isSuccess;
        }

        #endregion

        #region Event Details

        public List<EventInfo> GetEventInfo()
        {
            try
            {
                List<EventInfo> InfoObjs = new List<EventInfo>();

                InfoObjs.Add(new EventInfo(0, "Combine Event Log.", 100));
                InfoObjs.Add(new EventInfo(MeterEvent.MDIReset, 101, "Demand Reset.", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.Parameters, 102, "Parameterization.", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.OneWireTampering, 224, "Contactor Failure.", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.OneWireTampering, 103, "One Wire Tamper.", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.CustomerCode, 104, "Customer Code Change.", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.TimeChange, 105, "Time Change.", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.WindowSequenseChange, 106, "Window Sequence Change.", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.BillRegisterError, 107, "Bill Register Overflow.", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.ParamError, 108, "Param Error", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.OpticalportLogin, 109, "EMS Login", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.SystemReset, 110, "System Reset", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.PowerFail, 111, "Power outage Start", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.PowerFailEnd, 112, "Power outage End", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.Phasefail, 113, "Disconnection of Phases", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.OverVolt, 114, "Over Voltage", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.UnderVolt, 115, "Under Voltage Start", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.MDIExceed, 116, "MDI Exceed", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.ReverseEnergy, 117, "Reverse Energy Start", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.ReversePolarity, 118, "Reverse Polarity", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.OverCurrent, 119, "Over Current", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.ImbalanceVolt, 120, "Imbalance Voltage", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.CTFail, 121, "C.T Bypass", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.PTFail, 122, "PT Failure", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.HighNeutralCurrent, 123, "High Neutral Current", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.ImbalanceVolt, 124, "Phase Sequence", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.PowerFactorChange, 125, "Power Factor Change", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.BattreyLow, 126, "Battery Low", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.DoorOpen, 127, "Door Open.", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.TamperEnergy, 128, "Tamper Energy Start", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.OverLoad, 201, "Demand Over Load Start", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.MeterOnLoad, 203, "Meter On Load Start", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.MeterOnLoadEnd, 204, "Meter On Load End", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.UnderVoltEnd, 205, "Under Voltage End", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.SystemProblems, 206, "System Problems", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.PasswordChange, 207, "Password", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.OverLoad, 208, "Over Voltage End", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.ShortTimePowerFail, 209, "Short Time Power Fail Start", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.RecordRecoverd, 222, "Magnetic Field Start", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.RecordRecoverd, 210, "Record Recovered", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.TimeBaseEvent_1, 211, "Time Based Event 1", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.TimeBaseEvent_2, 212, "Time Based Event 2", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.ContactorStatusOn, 213, "Contactor Status On", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.ContactorStatusOff, 214, "Contactor Status Off", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.ShortTimePowerFailEnd, 215, "Short Time Power Fail End", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.ReverseEnergyEnd, 216, "Reverse Energy End", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.TamperEnergyEnd, 217, "Tamper Energy End", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.OverLoadEnd, 218, "Demand Over Load End", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.BillRegisterError, 220, "Bill Register Error", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.PhasePhail_End, 221, "Disconnection of Phases End", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.MDIOccurance, 219, "MDI Occurrence", 50));
                InfoObjs.Add(new EventInfo(MeterEvent.MDIOccurance, 223, "Magnetic Field End", 50));

                return InfoObjs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventLogInfo> GetEventLogInfo()
        {
            try
            {
                List<EventLogInfo> InfoObjs = new List<EventLogInfo>();
                InfoObjs.Add(new EventLogInfo(new EventInfo(0, "Combine Event Log", 100), Get_Index._Event_Log_All, Get_Index.Event_Counter_00));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.MDIReset, 101, "Demand Reset", 50), Get_Index._Event_Log_24, Get_Index.Event_Counter_24));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.Parameters, 102, "Parameterization", 50), Get_Index._Event_Log_25, Get_Index.Event_Counter_25));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.OneWireTampering, 224, "Contactor Failure", 50), Get_Index._Event_Log_17, Get_Index.Event_Counter_17));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.OneWireTampering, 103, "One Wire Tamper", 50), Get_Index._Event_Log_17, Get_Index.Event_Counter_17));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.CustomerCode, 104, "Customer Code Change", 50), Get_Index._Event_Log_27, Get_Index.Event_Counter_27));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.TimeChange, 105, "Time Change", 50), Get_Index._Event_Log_28, Get_Index.Event_Counter_28));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.WindowSequenseChange, 106, "Window Sequence Change", 50), Get_Index._Event_Log_29, Get_Index.Event_Counter_29));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.BillRegisterError, 107, "Bill Register Overflow", 50), Get_Index._Event_Log_31, Get_Index.Event_Counter_31));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.OpticalportLogin, 109, "EMS Login", 50), Get_Index._Event_Log_14, Get_Index.Event_Counter_14));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.SystemReset, 110, "System Reset", 50), Get_Index._Event_Log_22, Get_Index.Event_Counter_22));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.PowerFail, 111, "Power Outage Start", 50), Get_Index._Event_Log_15, Get_Index.Event_Counter_15));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.PowerFailEnd, 112, "Power Outage End", 50), Get_Index._Event_Log_16, Get_Index.Event_Counter_16));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.Phasefail, 113, "Disconnection of Phases", 50), Get_Index._Event_Log_04, Get_Index.Event_Counter_04));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.OverVolt, 114, "Over Voltage", 50), Get_Index._Event_Log_06, Get_Index.Event_Counter_06));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.UnderVolt, 115, "Under Voltage Start", 50), Get_Index._Event_Log_05, Get_Index.Event_Counter_05));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.MDIExceed, 116, "MDI Exceed", 50), Get_Index._Event_Log_21, Get_Index.Event_Counter_21));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.ReverseEnergy, 117, "Reverse Energy Start", 50), Get_Index._Event_Log_10, Get_Index.Event_Counter_10));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.ReversePolarity, 118, "Reverse Polarity", 50), Get_Index._Event_Log_03, Get_Index.Event_Counter_03));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.OverCurrent, 119, "Over Current", 50), Get_Index._Event_Log_07, Get_Index.Event_Counter_07));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.ImbalanceVolt, 120, "Imbalance Voltage", 50), Get_Index._Event_Log_01, Get_Index.Event_Counter_01));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.CTFail, 121, "C.T Bypass", 50), Get_Index._Event_Log_12, Get_Index.Event_Counter_12));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.PTFail, 122, "PT Failure", 50), Get_Index._Event_Log_13, Get_Index.Event_Counter_13));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.HighNeutralCurrent, 123, "High Neutral Current", 50), Get_Index._Event_Log_08, Get_Index.Event_Counter_08));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.PhaseSequence, 124, "Phase Sequence", 50), Get_Index._Event_Log_02, Get_Index.Event_Counter_02));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.PowerFactorChange, 125, "Power Factor Change", 50), Get_Index._Event_Log_33, Get_Index.Event_Counter_33));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.BattreyLow, 126, "Battery Low", 50), Get_Index._Event_Log_34, Get_Index.Event_Counter_34));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.DoorOpen, 127, "Door Open.", 50), Get_Index._Event_Log_35, Get_Index.Event_Counter_35));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.TamperEnergy, 128, "Tamper Energy Start", 50), Get_Index._Event_Log_11, Get_Index.Event_Counter_11));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.OverLoad, 201, "Demand Over Load Start", 50), Get_Index._Event_Log_09, Get_Index.Event_Counter_09));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.MeterOnLoad, 203, "Meter On Load Start", 50), Get_Index._Event_Log_18, Get_Index.Event_Counter_18));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.MeterOnLoadEnd, 204, "Meter On Load End", 50), Get_Index._Event_Log_19, Get_Index.Event_Counter_19));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.SystemProblems, 206, "System Problems", 50), Get_Index._Event_Log_23, Get_Index.Event_Counter_23));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.PasswordChange, 207, "Password", 50), Get_Index._Event_Log_26, Get_Index.Event_Counter_26));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.OverLoad, 208, "Over Voltage End", 50), Get_Index._Event_Log_30, Get_Index.Event_Counter_30));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.MeterOnLoad, 209, "Short Time Power Fail Start", 50), Get_Index._Event_Log_36, Get_Index.Event_Counter_36));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.RecordRecoverd, 222, "Magnetic Field Start", 50), Get_Index._Event_Log_37, Get_Index.Event_Counter_37));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.RecordRecoverd, 210, "Record Recovered", 50), Get_Index._Event_Log_37, Get_Index.Event_Counter_37));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.TimeBaseEvent_1, 211, "Time Based Event 1", 50), Get_Index._Event_Log_23, Get_Index.Event_Counter_23));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.TimeBaseEvent_2, 212, "Time Based Event 2", 50), Get_Index._Event_Log_26, Get_Index.Event_Counter_26));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.ContactorStatusOn, 213, "Contactor Status On", 50), Get_Index._Event_Log_30, Get_Index.Event_Counter_30));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.ContactorStatusOff, 214, "Contactor Status Off", 50), Get_Index._Event_Log_36, Get_Index.Event_Counter_36));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.ShortTimePowerFailEnd, 215, "Short Time Power Fail End", 50), Get_Index._Event_Log_37, Get_Index.Event_Counter_37));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.ReverseEnergyEnd, 216, "Reverse Energy End", 50), Get_Index._Event_Log_43, Get_Index.Event_Counter_43));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.TamperEnergyEnd, 217, "Tamper Energy End", 50), Get_Index._Event_Log_44, Get_Index.Event_Counter_44));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.OverLoadEnd, 218, "Demand Over Load End", 50), Get_Index._Event_Log_45, Get_Index.Event_Counter_45));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.BillRegisterError, 220, "Bill Register Error", 50), Get_Index._Event_Log_47, Get_Index.Event_Counter_47));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.PhasePhail_End, 221, "Disconnection of Phases End", 50), Get_Index._Event_Log_48, Get_Index.Event_Counter_48));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.MDIOccurance, 219, "MDI Occurrence", 50), Get_Index._Event_Log_46, Get_Index.Event_Counter_46));
                InfoObjs.Add(new EventLogInfo(new EventInfo(MeterEvent.MDIOccurance, 223, "Magnetic Field End", 50), Get_Index._Event_Log_46, Get_Index.Event_Counter_46));

                return InfoObjs;
            }
            catch (Exception ex)
            { throw ex; }
        }



        #endregion

        #region Delegate Handlers

        // This method shift to MDC ApplicationController
        public void EventNotification_Recieved(St_EventNotify StEventNotify)
        {
            //StOBISCode MajorAlarmProfile_OBISCode = Get_Index.MajorAlarmProfile_Event_Code;
            //StOBISCode MSN_OBISCode = Get_Index.Manufacturing_ID;
            //ApplicationController appContrLocal = null;

            //try
            //{
            //    if (ParentContainer != null &&
            //        ParentContainer is ApplicationController)
            //    {
            //        appContrLocal = ((ApplicationController)ParentContainer);
            //    }

            //    // Check Received OBIS Code
            //    // MajorAlarmProfile_Event_Code
            //    if (StEventNotify != null &&
            //        StEventNotify.COSEM_Object_OBISCode == MajorAlarmProfile_OBISCode)
            //    {
            //        if (!RecvMajorAlarmEventNotification)
            //        {
            //            // Do-not Process 
            //            // Major Alarm Event Notification
            //            return;
            //        }

            //        if (MajorAlarmEventDispatcher != null)
            //        {
            //            MajorAlarmNotification mjrAlarmNotify = null;
            //            if (MajorAlarmEventPool != null)
            //            {
            //                MajorAlarmEventPool.TryRemove<MajorAlarmNotification>(out mjrAlarmNotify);
            //            }

            //            if (mjrAlarmNotify == null)
            //                mjrAlarmNotify = new MajorAlarmNotification();

            //            mjrAlarmNotify.ReceptionTimeStamp = DateTime.Now;
            //            mjrAlarmNotify.OccurrenceTimeStamp = DateTime.MinValue;
            //            mjrAlarmNotify.EventCode = Convert.ToInt32(StEventNotify.Attribute_Value);
            //            if (StEventNotify.CaptureTime != null &&
            //                StEventNotify.CaptureTime.IsDateTimeConvertible)
            //                mjrAlarmNotify.OccurrenceTimeStamp = StEventNotify.CaptureTime.GetDateTime();

            //            mjrAlarmNotify.ReceptionTimeStamp = DateTime.Now;
            //            mjrAlarmNotify.DataToSave = RelatedDataToSave;

            //            if (appContrLocal != null)
            //            {
            //                appContrLocal.LogMessage("MajorAlarm Notification: \r\n" +
            //                                          mjrAlarmNotify.ToString(), "EVN", "RS", 1);

            //                if (appContrLocal.MeterInfo != null &&
            //                    appContrLocal.MeterInfo.MajorAlarmsProcessedInSession != null)
            //                {
            //                    // Add Current Major Alarm In List
            //                    if (!appContrLocal.MeterInfo.
            //                        MajorAlarmsProcessedInSession.Contains(mjrAlarmNotify.EventCode))
            //                    {
            //                        appContrLocal.MeterInfo.
            //                            MajorAlarmsProcessedInSession.Add(mjrAlarmNotify.EventCode);
            //                    }
            //                }
            //            }

            //            if (MajorAlarmEventDispatcher != null)
            //                MajorAlarmEventDispatcher.Dispatch<MajorAlarmNotification>(mjrAlarmNotify, 0);
            //        }
            //    }
            //    // Check Received OBIS Code
            //    // Serial Number
            //    else if (StEventNotify != null &&
            //             StEventNotify.COSEM_Object_OBISCode == MSN_OBISCode)
            //    {
            //        if (MajorAlarmEventDispatcher != null)
            //        {
            //            MeterSerialNumber SrNum = new MeterSerialNumber();
            //            SrNum.MSN = Convert.ToUInt32(StEventNotify.Attribute_Value);

            //            var msnNotify = new MSN_Notification()
            //            {
            //                MSN = SrNum,
            //                ReceptionTimeStamp = DateTime.Now,
            //                OccurrenceTimeStamp = DateTime.MinValue
            //            };

            //            if (StEventNotify.CaptureTime != null)
            //            {
            //                if (StEventNotify.CaptureTime.IsDateTimeConvertible)
            //                    msnNotify.OccurrenceTimeStamp = StEventNotify.CaptureTime.GetDateTime();
            //            }

            //            if (appContrLocal != null)
            //                appContrLocal.LogMessage("MSN Notification: \r\n" + msnNotify.ToString(), "GMN", "RS", 1);

            //            if (MajorAlarmEventDispatcher != null)
            //                MajorAlarmEventDispatcher.Dispatch<MSN_Notification>(msnNotify);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Extract Error Message
            //    String Error_message = ex.Message + ((ex.InnerException != null) ? ex.InnerException.Message : "NIL");

            //    if (_AP_Controller != null &&
            //        _AP_Controller.Logger != null)
            //        _AP_Controller.Logger.LogAPMessage("Error Process Event Notification " + Error_message, PacketType.EventNotification);
            //}
            //finally
            //{
            //}
        }

        #endregion

    }
}
