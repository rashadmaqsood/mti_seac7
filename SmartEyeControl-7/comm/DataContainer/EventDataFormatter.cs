using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using DLMS.Comm;

namespace comm
{
    public interface IEventDataFormatter
    {
        EventData MakeData(List<ILValue[]> CommObj);
        StOBISCode GetOBISCode(Get_Index OBIS_Index);
    }

    public class EventDataFormatter : IEventDataFormatter
    {
        #region Data_Members
        private List<EventInfo> eventInfo;
        public Func<Get_Index, StOBISCode> OBISLabelLookup;
        #endregion

        #region Constructurs
        public EventDataFormatter()
        {
            eventInfo = new List<EventInfo>();
        }
        public EventDataFormatter(List<EventInfo> eventInfoList)
        {
            eventInfo = new List<EventInfo>(eventInfoList);
        } 
        #endregion

        #region Properties
        public List<EventInfo> EventInfo
        {
            get { return eventInfo; }
            set { eventInfo = value; }
        }

        public List<StOBISCode> EventCountersOBISList
        {
            get
            {
                List<StOBISCode> obisCodeLST = new List<StOBISCode>(64);

                try
                {
                    foreach (var evInfo in EventInfo)
                    {
                        if (evInfo is EventLogInfo)
                            obisCodeLST.Add(GetOBISCode((evInfo as EventLogInfo).EventCounterIndex));
                    }
                }
                catch
                {
                }
                return obisCodeLST;
            }
        }

        public List<StOBISCode> EventLogsOBISList
        {
            get
            {
                List<StOBISCode> obisCodeLST = new List<StOBISCode>(64);

                try
                {
                    foreach (var evInfo in EventInfo)
                    {
                        if (evInfo is EventLogInfo)
                            obisCodeLST.Add(GetOBISCode((evInfo as EventLogInfo).EventLogIndex));
                    }
                }
                catch
                {
                }
                return obisCodeLST;
            }
        }

        #endregion

        #region IEventDataFormatter Members

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

        public EventData MakeData(List<ILValue[]> CommObj)
        {   
            EventInfo CurrentEventLogInfo = new EventLogInfo();
            try
            {
                if (CommObj == null || CommObj.Count <= 0)
                {
                    throw new Exception("No event data received");
                }
                EventData EventOccurences = new EventData();
                List<EventItem> EventInstances = new List<EventItem>();

                #region OBIS_CodesLst_To_Match

                var MajorAlarmProfile_Event_Code = GetOBISCode(Get_Index.MajorAlarmProfile_Event_Code);
                var Meter_Clock = GetOBISCode(Get_Index.Meter_Clock);
                var Event_Detail = GetOBISCode(Get_Index.Event_Detail);
                var Event_Counter = GetOBISCode(Get_Index.Event_Counter);
                var Event_Count_Total = GetOBISCode(Get_Index.Event_Count_Total);
                var Event_Counter_00 = GetOBISCode(Get_Index.Event_Counter_00);

                #endregion

                uint count = 1;
                foreach (var EventDataCapture in CommObj)
                {
                    EventItem EventOccurInstance = new EventItem();
                    EventOccurInstance.EventCounter = count;

                    ///Store Event code Details
                    ILValue TVal = Array.Find<ILValue>(EventDataCapture, (x) => x.OBIS_Index == Get_Index.MajorAlarmProfile_Event_Code);
                    if (TVal != null && TVal != null && TVal.Value is ValueType)
                    {
                        int eventCode = Convert.ToInt32(TVal.Value);
                        CurrentEventLogInfo = this.EventInfo.Find((x) => x.EventCode == eventCode);
                        if (CurrentEventLogInfo == null)
                        {
                            //continue;
                            ///throw new ArgumentNullException(String.Format("Unable to find Event Log Info for event code {0}", eventCode));
                            CurrentEventLogInfo = new EventInfo((MeterEvent)0, eventCode, "UnKnown(" + eventCode+")", -1);  //Code displayed with Unknown by Azeem v8.0.12
                            CurrentEventLogInfo.EventId = null;
                        }
                        else
                        {
                            EventOccurInstance.EventInfo = CurrentEventLogInfo;
                        }
                    }
                    ///Store  Event Instance Date & Time Stamp
                    TVal = Array.Find<ILValue>(EventDataCapture, (x) => x.OBIS_Index == Get_Index.Meter_Clock);
                    if (TVal != null &&  TVal.Value != null && TVal.Value.GetType() == typeof(StDateTime))
                    {
                        StDateTime timeStamp = (StDateTime)TVal.Value;
                        EventOccurInstance.DateTimeStamp = timeStamp;
                    }
                    
                    ///Store  Event Details Strings
                    TVal = Array.Find<ILValue>(EventDataCapture, (x) => x.OBIS_Index == Get_Index.Event_Detail);
                    if (TVal != null && TVal.Value != null && TVal.Value is Array)
                    {
                        EventOccurInstance.EventDetailString = (byte[])TVal.Value;
                    }
                    ///Decode Event Code 
                    ILValue TempTVAL = Array.Find<ILValue>(EventDataCapture, (x) => (x.OBIS_Index == Get_Index.MajorAlarmProfile_Event_Code));
                    if (TempTVAL != null)
                        TVal = TempTVAL;
                    else
                        TVal = Array.Find<ILValue>(EventDataCapture, (x) => x.OBIS_Index.ToString().Contains("Event_Code"));
                    
                    if (TVal != null)
                    {
                        int eventCode = Convert.ToInt32(TVal.Value);
                        if (EventOccurInstance.EventInfo.EventId == null)
                            EventOccurInstance.EventInfo.EventCode = eventCode;
                    }
                    // Decode Event Counter
                    TempTVAL = Array.Find<ILValue>(EventDataCapture, (x) => (x.OBIS_Index == Event_Counter ||
                                                                             x.OBIS_Index == Event_Count_Total ||
                                                                             x.OBIS_Index == Event_Counter_00));

                    if (TempTVAL != null)
                        TVal = TempTVAL;
                    else
                        TVal = Array.Find<ILValue>(EventDataCapture, (x) => x.OBIS_Index.ToString().Contains("Event_Count"));

                    if (TVal != null)
                    {
                        uint eventCounter = Convert.ToUInt32(TVal.Value);
                        EventOccurInstance.EventCounter = eventCounter;
                    }
                    EventOccurInstance.EventDetailStr = EventDetails(EventOccurInstance.EventDetailString, CurrentEventLogInfo);
                    EventOccurInstance.EventInfo = (EventInfo)CurrentEventLogInfo.Clone();
                    EventInstances.Add(EventOccurInstance);
                    count++;
                }
                EventOccurences.EventRecords = EventInstances;
                return EventOccurences;
            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException)
                    throw new Exception(ex.Message, ex);
                else
                {
                    throw new Exception("Error occurred generating Event data " + CurrentEventLogInfo, ex);
                }
            }
        }
        #endregion

        #region EventDetails_String
        private string EventDetails(byte[] EevntDetailStr, EventInfo Info)
        {
            string retValue = "";
            try
            {
                if (EevntDetailStr != null)
                {
                    foreach (byte item in EevntDetailStr)
                    {
                        retValue = String.Format("{0} {1}", retValue, item.ToString("X2"));
                    }
                }
               // return string.Format("{0} {1}", Info.EventCode, retValue);
                return string.Format("{0}" ,retValue);

            }
            catch (Exception)
            {
                return "!";
            }
        } 
        #endregion
    }

}
