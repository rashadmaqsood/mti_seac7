using System;
using System.Collections.Generic;
using System.Text;
using DLMS;

namespace comm
{
    public class BillingItem : ICloneable
    {
        #region DataMembers

        private Tariff value;

        public Tariff Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private float multiplier;

        public float Multiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }

        private Unit unit;

        public Unit Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        #endregion

        public BillingItem()
        {
            this.Value = new Tariff();
            this.Name = "x";
            this.Unit = Unit.UnitLess;
            this.Format = "f1";
            this.ValueInfo = new List<Get_Index>();
        }

        public BillingItem(BillingItem other)
        {
            this.Name = other.Name;
            this.Unit = other.Unit;
            this.Multiplier = other.Multiplier;
            if (other.Value != null)
                this.Value = (Tariff)other.Value.Clone();
            else
                this.Value = new Tariff();
            this.ValueInfo = new List<Get_Index>(other.ValueInfo);
            this.Format = other.Format;
        }

        private string format;

        public string Format
        {
            get { return format; }
            set { format = value; }
        }

        public override string ToString()
        {
            return this.Name;
        }

        #region ICloneable Members

        public object Clone()
        {
            BillingItem clonee = new BillingItem(this);
            return clonee;
        }

        #endregion

        private List<Get_Index> valueInfo;

        public List<Get_Index> ValueInfo
        {
            get { return valueInfo; }
            set { valueInfo = value; }
        }
    }

    //public enum Unit : short
    //{
    //    kWh = 0x01,
    //    kVah,
    //    kVarh,
    //    kW,
    //    kVa,
    //    kVar,
    //    UnitLess,
    //    V,
    //    A,
    //    Hz
    //}
}
