using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Comm
{
    public class MonitoredValue
    {
        #region Constructor

        public MonitoredValue()
        {
            Update(Get_Index.Dummy, 2);
        }

        public MonitoredValue(StOBISCode value, int attributeIndex)
        {
            Update(value, attributeIndex);
        }

        public MonitoredValue(MonitoredValue OtherObj)
        {
            if (OtherObj == null)
            {
                Update(Get_Index.Dummy, 2);
            }
            else
            {
                this.LogicalName = OtherObj.LogicalName;
                this.AttributeIndex = OtherObj.AttributeIndex;
                this.TargetDataType = OtherObj.TargetDataType;
            }
        }

        #endregion

        public void Update(StOBISCode value, int attributeIndex)
        {
            // ObjectType = value.ObjectType;
            this.LogicalName = value;
            this.AttributeIndex = attributeIndex;
        }

        public StOBISCode LogicalName
        {
            get;
            set;
        }

        public ushort ObjectType
        {
            get { return LogicalName.ClassId; }
        }

        public int AttributeIndex
        {
            get;
            set;
        }

        public DataTypes TargetDataType
        {
            get;
            set;
        }

        public override string ToString()
        {
            return LogicalName + " " + ObjectType.ToString() + " " + AttributeIndex.ToString();
        }
    }
}
