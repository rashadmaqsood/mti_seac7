using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DLMS.Comm
{
    /// <summary>
    /// Executed Scripts
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(ScheduleEntry))]
    public class ScheduleEntry
    {
        public const int WeekDaysCount = 07;
        public const int MaxSpecialDaysCount = 100;

        #region Constructor

        public ScheduleEntry()
        {
            // Default Schedule Entry
            Enable = true;

            Description = string.Empty;
            LogicalName = Get_Index.Dummy;
            SwitchTime = new StDateTime();
            ValidityWindow = 0xFFFF;    // any time

            ExecWeekdays = new BitArray(WeekDaysCount);
            ExecSpecialDays = new BitArray(MaxSpecialDaysCount);

            BeginDate = new StDateTime();
            EndDate = new StDateTime();
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="OtherObj"></param>
        public ScheduleEntry(ScheduleEntry OtherObj)
        {
            // Default Schedule Entry
            Enable = OtherObj.Enable;

            Description = OtherObj.Description;
            LogicalName = OtherObj.LogicalName;

            ScriptSelector = OtherObj.ScriptSelector;

            SwitchTime = new StDateTime();
            if (OtherObj.SwitchTime != null)
                SwitchTime = new StDateTime(OtherObj.SwitchTime);

            // Week Days
            if (OtherObj.ExecWeekdays != null &&
                OtherObj.ExecWeekdays.Count > 0)
                ExecWeekdays = new BitArray(OtherObj.ExecWeekdays);
            else
                ExecWeekdays = new BitArray(WeekDaysCount);

            // Special Days
            if (OtherObj.ExecSpecialDays != null &&
                OtherObj.ExecSpecialDays.Count > 0)
                ExecSpecialDays = new BitArray(OtherObj.ExecSpecialDays);
            else
                ExecSpecialDays = new BitArray(MaxSpecialDaysCount);

            BeginDate = new StDateTime();
            EndDate = new StDateTime();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Schedule Entry Index
        /// </summary>
        public ushort Index
        {
            get;
            set;
        }

        /// <summary>
        /// User Friendly Textual Description of Script Entry
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Is Schedule Entry Enabled
        /// </summary>
        public bool Enable
        {
            get;
            set;
        }

        /// <summary>
        /// Logical name of the Script table object
        /// </summary>
        public StOBISCode LogicalName
        {
            get;
            set;
        }

        /// <summary>
        /// Script identifier of the script to be executed 
        /// </summary>
        public byte ScriptSelector
        {
            get;
            set;
        }

        /// <summary>
        /// Accepts wildcards to define repetitive entries
        /// </summary>
        public StDateTime SwitchTime
        {
            get;
            set;
        }

        /// <summary>
        /// Defines a period in minutes, 
        /// in which an entry shall be processed after power fail 
        /// </summary>
        public ushort ValidityWindow
        {
            get;
            set;
        }

        /// <summary>
        /// Days of the week on which the entry is valid.
        /// </summary>
        public BitArray ExecWeekdays
        {
            get;
            set;
        }

        /// <summary>
        /// Perform the link to the IC �Special days table�, day_id.
        /// </summary>
        public BitArray ExecSpecialDays
        {
            get;
            set;
        }

        public int SpecialDaysCount
        {
            get
            {
                if (ExecSpecialDays == null)
                    return 0;
                else
                    return ExecSpecialDays.Count;
            }
        }


        /// <summary>
        /// Date starting period in which the entry is valid.
        /// </summary>
        public StDateTime BeginDate
        {
            get;
            set;
        }

        /// <summary>
        /// Date End period in which the entry is valid.
        /// </summary>
        public StDateTime EndDate
        {
            get;
            set;
        }

        #endregion

        #region ISerializable Members

        protected ScheduleEntry(SerializationInfo info, StreamingContext context)
        {
            // Default Constructor Initialize
            Enable = true;

            Description = string.Empty;
            StOBISCode LogicalNameLocal = Get_Index.Dummy;
            SwitchTime = null;
            ValidityWindow = 0xFFFF;      // any time

            BeginDate = null;
            EndDate = null;

            // Getting Schedule Entry

            // Serialize Constructor
            // Index
            this.Index = info.GetUInt16("Index");
            // Script Description
            this.Description = info.GetString("Description");
            // Schedule Entry
            this.Enable = info.GetBoolean("Enable");
            // Logical Name
            LogicalNameLocal.OBIS_Value = info.GetUInt64("LogicalName");
            this.LogicalName = LogicalNameLocal;
            // ScriptSelector
            this.ScriptSelector = info.GetByte("ScriptSelector");
            // StDateTime SwitchTime
            this.SwitchTime = (StDateTime)info.GetValue("SwitchTime", typeof(StDateTime));
            // ushort ValidityWindow
            this.ValidityWindow = info.GetUInt16("ValidityWindow");

            byte[] Temp_Value = new byte[01];

            // BitArray ExecWeekdays
            Temp_Value[0] = info.GetByte("ExecuteOnWeekdays");
            this.ExecWeekdays = new BitArray(Temp_Value);

            // int SpecialDaysCount
            //this.SpecialDaysCount = info.GetInt32("SpecialDaysCount");

            // BitArray ExecuteSpecialDays
            Temp_Value = (byte[])info.GetValue("ExecuteSpecialDays", typeof(byte[]));

            if (Temp_Value == null || Temp_Value.Length <= 0 || SpecialDaysCount <= 0)
                ExecSpecialDays = new BitArray(MaxSpecialDaysCount);
            else
                ExecSpecialDays = new BitArray(Temp_Value);

            // StDateTime BeginDate
            this.BeginDate = (StDateTime)info.GetValue("BeginDate", typeof(StDateTime));
            // StDateTime EndDate
            this.EndDate = (StDateTime)info.GetValue("EndDate", typeof(StDateTime));
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Adding Index
            info.AddValue("Index", this.Index, typeof(ushort));
            // Adding Script Description
            info.AddValue("Description", this.Description);
            // Adding Enable
            info.AddValue("Enable", this.Enable);
            // Adding Logical Name
            info.AddValue("LogicalName", this.LogicalName.OBIS_Value, typeof(ulong));
            // ScriptSelector
            info.AddValue("ScriptSelector", this.ScriptSelector, typeof(byte));

            // StDateTime SwitchTime
            info.AddValue("SwitchTime", this.SwitchTime, typeof(StDateTime));

            // ushort ValidityWindow
            info.AddValue("ValidityWindow", this.ValidityWindow, typeof(ushort));

            // BitArray ExecWeekdays
            byte[] Temp_Value = null;
            Temp_Value = DLMS_Common.BitArrayToByteArray(this.ExecWeekdays);
            info.AddValue("ExecuteOnWeekdays", Temp_Value[0], typeof(byte));

            // int SpecialDaysCount
            info.AddValue("SpecialDaysCount", this.SpecialDaysCount, typeof(int));

            // BitArray ExecSpecialDays
            Temp_Value = null;
            Temp_Value = DLMS_Common.BitArrayToByteArray(this.ExecSpecialDays);
            info.AddValue("ExecSpecialDays", Temp_Value, typeof(byte[]));

            // StDateTime BeginDate
            info.AddValue("BeginDate", this.BeginDate, typeof(StDateTime));

            // StDateTime EndDate
            info.AddValue("EndDate", this.EndDate, typeof(StDateTime));
        }

        #endregion


        /// <summary>
        /// Clone Schedule Entry
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new ScheduleEntry(this);
        }
    }

    public struct ScheduleEntryRange
    {
        public ushort FirstIndex;
        public ushort LastIndex;
    }

}
