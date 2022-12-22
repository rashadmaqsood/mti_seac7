using System;
using System.Collections.Generic;
using System.Text;
using DLMS;
using SharedCode.Comm.DataContainer;
using SharedCode.Common;
using SharedCode.Comm.Param;

namespace SharedCode.Comm.DataContainer
{
    public class BillingItem : ICloneable, IParam
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
            this.DBInfo = new List<string>();
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
            this.DBInfo = new List<string>(other.DBInfo);
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

        private List<string> dbInfo;

        public List<string> DBInfo
        {
            get { return dbInfo; }
            set { dbInfo = value; }
        }

        public string DBColumnsString
        {
            get
            {
                StringBuilder columns = new StringBuilder();
                if (valueInfo.Count == 1 || valueInfo.Count == 5)
                {
                    foreach (string val in DBInfo)
                    {
                        if (string.IsNullOrEmpty(val))
                        {
                            columns.Clear();
                            break;
                        }
                        columns.Append(string.Format("`{0}`,", val));
                    }
                }
                return columns.ToString();
            }
        }

        public string DBValuesString
        {
            get
            {
                StringBuilder dbValues = new StringBuilder();
                if (valueInfo.Count == 1)
                {
                    dbValues.Append(string.Format("'{0}',", Commons.Validate_BillData(Value.T1)));
                }
                else if (valueInfo.Count == 5)
                {
                    dbValues.Append(string.Format("'{0}',", Commons.Validate_BillData(Value.T1)));
                    dbValues.Append(string.Format("'{0}',", Commons.Validate_BillData(Value.T2)));
                    dbValues.Append(string.Format("'{0}',", Commons.Validate_BillData(Value.T3)));
                    dbValues.Append(string.Format("'{0}',", Commons.Validate_BillData(Value.T4)));
                    dbValues.Append(string.Format("'{0}',", Commons.Validate_BillData(Value.TL)));
                }
                return dbValues.ToString();
            }
        }

        public string DBUpdateString
        {
            get
            {
                StringBuilder dbValues = new StringBuilder();
                if (valueInfo.Count == 1 && DBInfo.Count == 1)
                {
                    dbValues.Append($"{DBInfo[0]} = '{Commons.Validate_BillData(Value.T1)}',");
                }
                else if (valueInfo.Count == 5 && DBInfo.Count == 5)
                {
                    dbValues.Append($"{DBInfo[0]} = '{Commons.Validate_BillData(Value.T1)}',");
                    dbValues.Append($"{DBInfo[1]} = '{Commons.Validate_BillData(Value.T2)}',");
                    dbValues.Append($"{DBInfo[2]} = '{Commons.Validate_BillData(Value.T3)}',");
                    dbValues.Append($"{DBInfo[3]} = '{Commons.Validate_BillData(Value.T4)}',");
                    dbValues.Append($"{DBInfo[4]} = '{Commons.Validate_BillData(Value.TL)}',");
                }
                return dbValues.ToString();
            }
        }


    }

    // public enum Unit : short
    // {
    //     kWh = 0x01,
    //     kVah,
    //     kVarh,
    //     kW,
    //     kVa,
    //     kVar,
    //     UnitLess,
    //     V,
    //     A,
    //     Hz
    // }
}
