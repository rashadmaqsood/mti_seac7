using DLMS.Comm;
using DLMS;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedCode.Controllers
{
    public class EventController_YTL : EventController
    {
        public override bool TryReadEventLogDataInChunks(Profile_Counter Event_Counters, EventInfo EventLogInfoObj, EventData EventInstances,
    Action<Exception> SetInnerException = null, uint maxChunkSize = 25, CancellationTokenSource CancelToken = null)
        {
            Exception InnerException = null;
            bool IsSuccess = false;
            List<ILValue[]> EventRawData = new List<ILValue[]>();
            EventData tempEventData = new EventData();
            try
            {
                EventLogInfo infoObj = null;
                if (EventLogInfoObj is EventLogInfo)
                    infoObj = (EventLogInfo)EventLogInfoObj;
                else
                    infoObj = GetEventLogInfo(EventLogInfoObj);
                EventInstances.EventInfo = infoObj;
                //Update Current Event Counter
                try
                {
                    EventRawData.Clear();
                    ///Read Internal Events Data In Chunks
                    IsSuccess = TryReadEventLogDataRaw(null, infoObj, EventRawData, (ex) => InnerException = ex);

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
                InnerException = new Exception(String.Format("Error occurred while reading events {0} records data (Error Code:{1})", EventLogInfoObj, (int)MDCErrors.App_Events_Data_Read), ex);
            }
            finally
            {
                if (SetInnerException != null && InnerException != null)
                    SetInnerException(InnerException);
            }
            return IsSuccess;
        }


        public override bool TryReadEventLogData(Profile_Counter Event_Counters, EventInfo EventLogInfoObj, EventData EventInstances,
    Action<Exception> SetInnerException = null, CancellationTokenSource CancelToken = null)
        {
            Exception InnerException = null;
            bool IsSuccess = false;
            List<ILValue[]> EventRawData = new List<ILValue[]>();
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
                if (SetInnerException != null && InnerException != null)
                    SetInnerException(InnerException);
            }
            return IsSuccess;
        }

    }
}
