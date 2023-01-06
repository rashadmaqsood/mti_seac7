using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dlmsConfigurationTool.Common
{
    public class Data_Counters
    {
        public const long Max_LPDataSize = 4632;

        #region Properties
        
        public long Min_LP_Counter { get; set; }
        public long Max_LP_Counter { get; set; }
        public long Count_LP_Counter { get; set; }

        public List<Counter_Range> Missing_LP_Data { get; set; }
        public List<long> LP_CounterData { get; set; }
        public bool IsDataValidated { get; set; } 
        
        #endregion

        public Data_Counters()
        {
            Min_LP_Counter = Max_LP_Counter = 0;

            Missing_LP_Data = new List<Counter_Range>();
            LP_CounterData = new List<long>();
            IsDataValidated = false;
        }

        public bool TryValidatedLoadProfileData(List<long> LP_CounterData)
        {
            try
            {
                this.LP_CounterData = LP_CounterData;

                if (LP_CounterData.Count <= 1)
                    return false;
                LP_CounterData.Sort();
                Max_LP_Counter = LP_CounterData.Max();
                Min_LP_Counter = LP_CounterData.Min();
                Counter_Range Mis_Range;

                //if (Min_LP_Counter > 1)
                //{
                //    Mis_Range = new Counter_Range() { Min_LP = 1, Max_LP = (Min_LP_Counter - 1) };
                //    Missing_LP_Data.Add(Mis_Range);
                //}

                for (int index = 0; index < LP_CounterData.Count; index++)
                {
                    if (LP_CounterData[index] + 1 == LP_CounterData[index + 1])
                        continue;
                    else
                    {
                        Mis_Range = new Counter_Range() { Min_LP = LP_CounterData[index] + 1, Max_LP = LP_CounterData[index + 1] - 1 };
                        Missing_LP_Data.Add(Mis_Range);
                    }
                }
                IsDataValidated = (Missing_LP_Data.Count > 0);
                return IsDataValidated;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public struct Counter_Range
        {
            public long Min_LP;
            public long Max_LP;
            public long Count;
        }
    }
}
