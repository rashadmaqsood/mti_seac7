using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Comm
{
    public class ActionItem
    {
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ActionItem()
        {
            LogicalName = Get_Index.Dummy;
            ScriptSelector = 0;
        }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="LogicalNameArg"></param>
        /// <param name="ScriptSelectorArg"></param>
        public ActionItem(StOBISCode LogicalNameArg, ushort ScriptSelectorArg)
        {
            this.LogicalName = LogicalNameArg;
            this.ScriptSelector = ScriptSelectorArg;
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="OtherObj"></param>
        public ActionItem(ActionItem OtherObj)
        {
            this.LogicalName = OtherObj.LogicalName;
            this.ScriptSelector = OtherObj.ScriptSelector;
        }

        #endregion


        public StOBISCode LogicalName
        {
            get;
            set;
        }

        public ushort ScriptSelector
        {
            get;
            set;
        }

        public override string ToString()
        {
            return LogicalName + " " + ScriptSelector.ToString();
        }
    }

    public class ActionSet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ActionSet()
        {
            ActionUp = new ActionItem();
            ActionDown = new ActionItem();
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        public ActionSet(ActionSet OtherObj)
        {
            if (OtherObj.ActionUp != null)
                ActionUp = new ActionItem(OtherObj.ActionUp);
            else
                ActionUp = new ActionItem();

            if (OtherObj.ActionDown != null)
                ActionDown = new ActionItem(OtherObj.ActionDown);
            else
                ActionDown = new ActionItem();
        }

        #region Properties

        public ActionItem ActionUp
        {
            get;
            set;
        }

        public ActionItem ActionDown
        {
            get;
            set;
        }

        #endregion
    }
}
