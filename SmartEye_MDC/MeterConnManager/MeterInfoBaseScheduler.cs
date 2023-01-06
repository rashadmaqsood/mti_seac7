using comm;
using SharedCode.Comm.DataContainer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communicator.MeterConnManager
{
    public abstract class MeterInfoBaseScheduler : IEnumerable<MeterSerialNumber>, IDisposable
    {
        private List<MeterSerialNumber> meterList = null;
        private List<MeterSerialNumber> scheduledMeterList = null;
        protected ConcurrentDictionary<MeterSerialNumber, MeterInformation> meterSchedules = null;

        public Func<MeterSerialNumber, MeterInformation> UpdateMeterInformation;

        #region Properties

        public List<MeterSerialNumber> MeterList
        {
            get { return meterList; }
            set { meterList = value; }
        }

        public List<MeterSerialNumber> ScheduledMeterList
        {
            get { return scheduledMeterList; }
            protected set { scheduledMeterList = value; }
        }

        #endregion

        #region Constructor

        public MeterInfoBaseScheduler()
        {
            meterList = new List<MeterSerialNumber>(10);
            scheduledMeterList = new List<MeterSerialNumber>(10);
            meterSchedules = new ConcurrentDictionary<MeterSerialNumber, MeterInformation>();
        }

        #endregion

        #region Support_Methods

        public void ResetSchedule()
        {
            if (scheduledMeterList != null)
            {
                scheduledMeterList.Clear();
            }

            scheduledMeterList = null;
        }

        public void ClearMeterInformation()
        {
            try
            {
                // meterSchedules
                if (meterSchedules != null)
                {
                    foreach (var item in meterSchedules)
                    {
                        if (item.Value != null)
                            item.Value.Dispose();
                    }

                    meterSchedules.Clear();
                    meterSchedules = null;
                }
            }
            catch { }
        }

        public void ClearMeterList()
        {
            if (meterList != null)
            {
                meterList.Clear();
            }
        }

        public void ClearAll()
        {
            ResetSchedule();
            ClearMeterInformation();
            ClearMeterList();
        }

        #endregion

        #region Member_Methods

        public MeterInformation GetMeterInformation(MeterSerialNumber msn, bool updateMeterInfo = false)
        {
            bool isUPdateInfo = updateMeterInfo;
            MeterInformation info = null;

            try
            {
                if (msn == null)
                    throw new ArgumentNullException("MeterSerialNumber");

                if (meterSchedules != null &&
                    meterSchedules.ContainsKey(msn))
                {
                    meterSchedules.TryGetValue(msn, out info);
                }

                isUPdateInfo = isUPdateInfo || (info == null);
                if (isUPdateInfo)
                {
                    if (UpdateMeterInformation == null)
                        throw new ArgumentNullException("UpdateMeterInformation");

                    var New_info = UpdateMeterInformation(msn);

                    if (info == null)
                        meterSchedules.TryAdd(msn, New_info);
                    else
                        meterSchedules.TryUpdate(msn, New_info, info);

                    info = New_info;
                }
            }
            catch
            {
                // raise error
            }
            finally
            {
            }

            return info;
        }

        public MeterInformation RemoveMeterInformation(MeterSerialNumber msn)
        {
            MeterInformation info = null;

            try
            {
                if (msn == null)
                    throw new ArgumentNullException("MeterSerialNumber");

                if (meterSchedules != null && meterSchedules.ContainsKey(msn))
                    meterSchedules.TryRemove(msn, out info);
            }
            catch
            {
                // raise error
            }
            finally
            {
            }

            return info;
        }

        /// <summary>
        /// ExecuteScheduler To Implement Scheduler Business Logic in function 
        /// to define the order in which meters to be processed
        /// </summary>
        public virtual void ExecuteScheduler()
        {
            try
            {
                scheduledMeterList = null;
                // Do Nothing Business Logic
                scheduledMeterList = new List<MeterSerialNumber>(MeterList);
            }
            catch { }
        }

        #endregion

        #region IEnumerable<MeterSerialNumber>

        public IEnumerator<MeterSerialNumber> GetEnumerator()
        {
            // Exec Meter Scheduler
            if (scheduledMeterList == null ||
                scheduledMeterList.Count < 0)
                ExecuteScheduler();

            return scheduledMeterList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            // Exec Meter Scheduler
            if (scheduledMeterList == null ||
                scheduledMeterList.Count <= 0)
                ExecuteScheduler();

            return scheduledMeterList.GetEnumerator();
        }

        #endregion

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                // release unmanaged memory  
                if (disposing)
                {
                    // release other disposable objects  
                    // if (resource != null) resource.Dispose();
                    scheduledMeterList = null;
                    meterList = null;

                    // meterSchedules
                    ClearMeterInformation();

                    #region //Remove UpdateMeterInformation Event Handlers

                    Delegate[] Handlers = null;
                    if (UpdateMeterInformation != null)
                    {
                        Handlers = UpdateMeterInformation.GetInvocationList();
                        foreach (Delegate item in Handlers)
                        {
                            UpdateMeterInformation -= (Func<MeterSerialNumber, MeterInformation>)item;
                        }
                    }

                    #endregion
                }
            }
            // Do Not Raise
            catch
            {
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }

    /// <summary>
    /// Simple SimpleMeterInfoScheduler Only Schedule Meter reading based on the Meters Serial Numbers Info Only
    /// </summary>
    public class SimpleMeterInfoScheduler : MeterInfoBaseScheduler
    {
        public SimpleMeterInfoScheduler(): base()
        {
            ScheduledMeterList = new List<MeterSerialNumber>(MeterList);
        }

        public override void ExecuteScheduler()
        {
            try
            {
                ScheduledMeterList = null;
                // Do Nothing Business Logic
                ScheduledMeterList = new List<MeterSerialNumber>(MeterList);
                ScheduledMeterList.Sort(new Comparison<MeterSerialNumber>((x, y) => x.MSN.CompareTo(y.MSN)));
            }
            catch { }
        }
    }
}
