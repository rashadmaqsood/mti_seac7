using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedCode.Comm.DataContainer;

namespace SharedCode.Comm.DataContainer
{
    public class InstantaneousItem
    {
        #region DataMembers

        private Phase value;
        private string name;
        private Unit unit;
        private float multiplier;
        private string format;

        public string Format
        {
            get { return format; }
            set { format = value; }
        }
        public float Multiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Unit _Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        public Phase Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        #endregion

        public InstantaneousItem()
        {
            this.Value = new Phase();
            this.Name = "x";
            this._Unit = Unit.UnitLess;
            this.Format = "f1";
        }

    }
    public enum Unit : short
    {
        kWh = 0x01,
        kVah,
        kVarh,
        kW,
        kVa,
        kVar,
        UnitLess,
        V,
        A,
        Hz
    }
}
