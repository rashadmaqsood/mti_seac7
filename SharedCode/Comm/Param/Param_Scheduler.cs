using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SharedCode.Comm.Param
{
    [Serializable]
    [XmlInclude(typeof(ScheduleEntry))]
    [XmlInclude(typeof(StDateTime))]
    public class Param_Scheduler : ICloneable, ISerializable
    {
        #region Data_Member

        private List<ScheduleEntry> _schedulerEntries;

        #endregion

        #region Properties

        public List<ScheduleEntry> SchedulerEntries
        {
            get { return _schedulerEntries; }
            set { _schedulerEntries = value; }
        }

        #endregion

        public Param_Scheduler()
        {
            _schedulerEntries = new List<ScheduleEntry>();
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        public Param_Scheduler(Param_Scheduler OtherObj)
        {
            _schedulerEntries = new List<ScheduleEntry>();
            ScheduleEntry scptCurrent = null;

            foreach (var entry in _schedulerEntries)
            {
                scptCurrent = new ScheduleEntry(entry);
                _schedulerEntries.Add(scptCurrent);
            }
        }

        #region ISerializable Members

        protected Param_Scheduler(SerializationInfo info, StreamingContext context)
        {
            // Default Constructor Initialize
            _schedulerEntries = new List<ScheduleEntry>(01);

            // Serialize Constructor
            // Schedule Entry Item Count
            int schItemCount = info.GetInt32("ScheduleEntryItemCount");

            ScheduleEntry schEntryItem = null;

            // Get Array Member Elements
            for (int indexer = 0;
                 indexer < schItemCount;
                 indexer++)
            {
                schEntryItem = (ScheduleEntry)info.GetValue(string.Format("Element_{0}", indexer), typeof(ScheduleEntry));
                this._schedulerEntries.Add(schEntryItem);
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Adding Script Counts
            info.AddValue("ScheduleEntryItemCount", _schedulerEntries.Count);

            // Add Array Member Elements
            for (int indexer = 0;
                 indexer < this._schedulerEntries.Count;
                 indexer++)
            {
                info.AddValue(string.Format("Element_{0}", indexer), this._schedulerEntries[indexer], typeof(ScheduleEntry));
            }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return new Param_Scheduler(this);
        }

        #endregion

    }
}
