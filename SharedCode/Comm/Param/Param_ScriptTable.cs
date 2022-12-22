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
    [XmlInclude(typeof(Script))]
    public class Param_ScriptTable : ICloneable, ISerializable
    {
        #region Data_Member

        private List<Script> _scripts;

        #endregion

        #region Properties

        public List<Script> Scripts
        {
            get { return _scripts; }
            set { _scripts = value; }
        }

        #endregion

        public Param_ScriptTable()
        {
            _scripts = new List<Script>();
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        public Param_ScriptTable(Param_ScriptTable OtherObj)
        {
            _scripts = new List<Script>();
            Script scptCurrent = null;

            foreach (var scpt in _scripts)
            {
                scptCurrent = new Script(scpt);
                _scripts.Add(scptCurrent);
            }
        }

        #region ISerializable Members

        protected Param_ScriptTable(SerializationInfo info, StreamingContext context)
        {
            // Default Constructor Initialize
            _scripts = new List<Script>(01);

            // Serialize Constructor
            // Script List Count
            int scptLstCount = info.GetInt32("ScriptListCount");

            Script scrpt = null;

            // Get Array Member Elements
            for (int indexer = 0;
                 indexer < scptLstCount;
                 indexer++)
            {
                scrpt = (Script)info.GetValue(string.Format("Element_{0}", indexer), typeof(Script));
                this._scripts.Add(scrpt);
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Adding Script Counts
            info.AddValue("ScriptListCount", _scripts.Count);

            Script scrpt = null;

            // Add Array Member Elements
            for (int indexer = 0;
                 indexer < this._scripts.Count;
                 indexer++)
            {
                info.AddValue(string.Format("Element_{0}", indexer), this._scripts[indexer]);
            }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return new Param_ScriptTable(this);
        }

        #endregion

    }
}
