#define Enable_DEBUG_ECHO
#define Enable_Read_Param_Log
#define Enable_Error_Logging
//#define Enable_DEBUG_RunMode
// #define Enable_Transactional_Logging
// #define Enable_LoadTester_Mode
#define Enable_Abstract_Log

using SharedCode.Comm.DataContainer;
using SharedCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Communicator.MeterConnManager
{
    public class ApplicationController_YTL : ApplicationController
    {
        protected override bool ProcessEventsLogRequest(CancellationTokenSource CancelTokenSource, int meterEvetnsCount)
        {
            bool IsProcessed = false;
            if (!MeterInfo.PrioritizeWakeup && MeterInfo.read_individual_events_sch || MeterInfo.Schedule_EV.IsSuperImmediate || (MeterInfo.ReadEventsForcibly && MeterInfo.Read_EV && MeterInfo.Save_EV))
            {
                if (MeterInfo.read_logbook && (MeterInfo.ReadEventsForcibly || MeterInfo.Schedule_EV.IsSuperImmediate ||
                    MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_EV)))
                {
                    bool lastTimeUpdate = false;
                    var EventsData = new EventData();
                    //bool IsEventsUpToDate = false;
                    bool IsEventsReadSuccessfully = false;
                    //long MeterCount = 0;
                    try
                    {
                        if (MeterInfo.EV_Counters.DB_Counter >= 0)
                        {
                            DateTime latest_event_logbook = DateTime.MinValue;
                            #region Making Entry

                            var e_info = new EventInfo();
                            e_info.EventCode = 0;
                            e_info = Event_Controller.EventLogInfoList.Find((x) => x.EventCode == 0);


                            #endregion
                            #region Read

                            Exception innerException = null;
                            try
                            {

                                #region Reading Events Data from
#if Enable_Abstract_Log
                                LogMessage(String.Format("Reading Events Data"), (MeterInfo.ReadEventsForcibly) ? "EDMA" : "ED",
                                    string.Format("R"), 1);
#endif

#if !Enable_Abstract_Log
                                    LogMessage(String.Format("Reading Events Data from {0} to {1} {2}", eventCounter.Previous_Counter, eventCounter.Current_Counter, (MeterInfo.ReadEventsForcibly) ? "due to some Major Alarm occurred" : ""), 0);
#endif
                                #endregion
                                IsEventsReadSuccessfully = Event_Controller.TryReadEventLogDataInChunks(null, e_info, EventsData,
                                                (ex) => innerException = ex, 25, CancelTokenSource);

                                if (IsEventsReadSuccessfully)
                                {
                                    #region Events Data read complete
#if Enable_Abstract_Log
                                    LogMessage("Events Data read complete", (MeterInfo.ReadEventsForcibly) ? "EDMA" : "ED", "S", 3);
#endif

#if !Enable_Abstract_Log
                                        LogMessage("Events Data read complete", 3);
#endif
                                    #endregion
                                    //Update Event For Live-Update

                                    //EventIDtoCode idTOcode = new EventIDtoCode();
                                    DateTime latest = DateTime.MinValue;
                                    int id = 0;
                                    bool retry = true;
                                    EventItem data = null;

                                    EventData copy_data = EventsData.Clone();

                                    #region Contactor Status Update
                                    var lastState = (from ev in copy_data.EventRecords
                                                     where ev.EventInfo.EventCode == 213 || ev.EventInfo.EventCode == 214
                                                     orderby ev.EventDateTimeStamp descending
                                                     select ev).FirstOrDefault();

                                    if (lastState != null)
                                    {
                                        var status = -1;
                                        if (lastState.EventInfo.EventCode == 213)
                                            status = 1;
                                        else
                                            status = 0;
                                        MeterInfo.Current_contactor_status = status;
                                        MeterInfo.Schedule_CO.LastReadTime = DateTime.Now;
                                        MIUF.IsContactorStatusUpdate = true;
                                    }
                                    #endregion

                                    while (retry && copy_data.EventRecords.Count > 0)
                                    {
                                        id = 0;
                                        latest = copy_data.EventRecords.Max(x => x.EventDateTimeStamp);
                                        data = copy_data.EventRecords.Find(x => x.EventDateTimeStamp == latest);

                                        //id = idTOcode.getEventID(data.EventInfo.EventCode);
                                        if (data != null && data.EventInfo != null)
                                            id = (int)(data.EventInfo.EventId ?? 0);

                                        MeterInfo.eventsForLiveUpdate_logbook = Commons.HexStringToBinary(MeterInfo.eventsForLiveUpdate_logbook_string, meterEvetnsCount);
                                        if (id > 0 && MeterInfo.eventsForLiveUpdate_logbook[id - 1] == '1')
                                        {
                                            retry = false;
                                        }
                                        else
                                        {
                                            copy_data.EventRecords.Remove(data);
                                        }
                                    }
                                    if (!retry)
                                    {
                                        if (DB_Controller.UpdateLastEvent_Live_logbook(MeterInfo.MSN, data.EventInfo.EventCode, data.EventDateTimeStamp))
                                        {
                                            #region Logbook->Event Code " + data.EventInfo.EventCode + " updated to Instantaneous Live
#if Enable_Abstract_Log
                                            LogMessage("Logbook->Event Code " + data.EventInfo.EventCode + " updated to Instantaneous Live ", "LEDL", data.EventInfo.EventCode.ToString(), 1);
#endif

#if !Enable_Abstract_Log
                                                LogMessage("Logbook->Event Code " + data.EventInfo.EventCode + " updated to Instantaneous Live ");
#endif
                                            #endregion
                                        }
                                        else
                                        {
                                            #region Error occurred while updating Event Code
#if Enable_Abstract_Log
                                            LogMessage("Error occurred while updating Event Code " + data.EventInfo.EventCode + " updated to Instantaneous Live ", "LEDL", "F", 1);
#endif
#if !Enable_Abstract_Log
                                                LogMessage("Error occurred while updating Event Code " + data.EventInfo.EventCode + " updated to Instantaneous Live ");
#endif
                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        #region No events to read from the logbook
#if Enable_Abstract_Log
                                        LogMessage("No events to read from the logbook", "LEDL", "EMPT", 1);
#endif

#if !Enable_Abstract_Log
                                            LogMessage("No events to read from the logbook");
#endif
                                        #endregion
                                    }
                                }
                                if (!IsEventsReadSuccessfully && innerException != null)
                                    throw innerException;
                            }
                            catch (Exception ex)
                            {
                                LogMessage("Error while reading Events Data: " + ex.Message, 4);
                                throw;
                            }
                            if (innerException != null)
                            {
                                throw innerException;
                            }

                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        IsProcessed = false;
                        LogMessage(ex, 4, "Combine Events Log");
                        //if (!(ex is NullReferenceException))
                        //    throw;
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        // MeterInfo.ReadEventsForcibly = false;
                        #region Saving To Database
                        try
                        {
                            if (IsEventsReadSuccessfully)
                                ResetMaxIOFailure();
                            if (IsEventsReadSuccessfully)
                            {
                                if (MeterInfo.Save_EV && EventsData != null)
                                {
                                    uint tempMAXEventCount = EventsData.MaxEventCounter;
                                    CustomException CEX = DB_Controller.saveEventsDataWithReplace(EventsData, MeterInfo.MSN, SessionDateTime, MeterInfo, MIUF);

                                    #region Saving Events Data failed
#if Enable_Abstract_Log
                                    LogMessage(String.Format("Saving Events Data failed, {0} (Error Code:{1})", CEX.SomeMessage, (int)MDCErrors.App_Events_Data_Save), "EDD", "F", 1);
#endif

#if !Enable_Abstract_Log
                                            LogMessage(String.Format("Saving Events Data failed, {0} (Error Code:{1})", CEX.SomeMessage, (int)MDCErrors.App_Events_Data_Save));
#endif
                                    #endregion
                                }
                            }
                            if ((MeterInfo.EV_Counters.IsUptoDate || IsEventsReadSuccessfully) && MeterInfo.Save_EV)//!MeterInfo.read_individual_events_sch)change by furqan
                            {
                                MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_EV, SessionDateTime);
                                MIUF.Schedule_EV = true;
                                MIUF.last_EV_time = lastTimeUpdate | MeterInfo.EV_Counters.IsUptoDate;
                                if (MeterInfo.Schedule_EV.IsSuperImmediate)
                                    MIUF.SuperImmediate_EV = true;
                                if (MeterInfo.Schedule_EV.SchType == ScheduleType.IntervalFixed ||
                                    MeterInfo.Schedule_EV.SchType == ScheduleType.IntervalRandom)
                                    MIUF.base_time_EV = true;
                                MeterInfo.Schedule_EV.IsSuperImmediate = false;
                            }
                            IsProcessed = true;
                        }
                        catch (Exception ex)
                        {
                            LogMessage(ex.InnerException, 4);
                        }
                        #endregion
                    }
                Exit:
                    #region Events Disabled
#if Enable_Abstract_Log
                    // if (!MeterInfo.Read_EV)
                    // LogMessage("Events Disabled", "SVRDSEVIVCOUNT", "DSEV", 1);
                    ;
#endif

#if !Enable_Abstract_Log
                    if (!MeterInfo.Read_EV)
                        LogMessage("Events Disabled", 3);
#endif
                    #endregion
                }
            }

            return IsProcessed;
        }

    }
}
