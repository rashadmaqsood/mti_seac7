using comm;
using SharedCode.Comm.DataContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communicator.MeterConnManager
{
    public class DateTimeValuesComparer : IComparer<KeyValuePair<MeterSerialNumber, List<DateTime>>>
    {
        public int Compare(KeyValuePair<MeterSerialNumber, List<DateTime>> x, KeyValuePair<MeterSerialNumber, List<DateTime>> y)
        {
            if (x.Key == null || x.Value == null || x.Value.Count <= 0)
            {
                if (y.Key == null || y.Value == null || y.Value.Count <= 0)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y.Key == null || y.Value == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    int sumCompValX = 0;
                    int sumCompValY = 0;
                    int resltValue = 0;

                    DateTime Disable_ScheduleDateTime_Val = DateTime.MaxValue;
                    DateTime X_DateTime_Val = DateTime.MaxValue;
                    DateTime Y_DateTime_Val = DateTime.MaxValue;

                    for (int index = 0; index < x.Value.Count || index < y.Value.Count; index++)
                    {
                        if (index < x.Value.Count)
                            X_DateTime_Val = x.Value[index];
                        else
                            X_DateTime_Val = Disable_ScheduleDateTime_Val;


                        if (index < y.Value.Count)
                            Y_DateTime_Val = y.Value[index];
                        else
                            Y_DateTime_Val = Disable_ScheduleDateTime_Val;


                        resltValue = X_DateTime_Val.CompareTo(Y_DateTime_Val);

                        if (resltValue > 0)
                            sumCompValX += 1;
                        else if (resltValue < 0)
                            sumCompValY += 1;
                        else
                        {
                            // Either Add 1 to both or 0
                            ;
                        }
                    }

                    // Compare Result X & Result Y
                    resltValue = sumCompValX.CompareTo(sumCompValY);
                    return resltValue;
                }
            }
        }
    }

    public class BatchProcessorComparer : IComparer<KeyValuePair<MeterSerialNumber, List<int>>>
    {
        public int Compare(KeyValuePair<MeterSerialNumber, List<int>> x, KeyValuePair<MeterSerialNumber, List<int>> y)
        {
            if (x.Key == null || x.Value == null || x.Value.Count <= 0)
            {
                if (y.Key == null || y.Value == null || y.Value.Count <= 0)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y.Key == null || y.Value == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    int sumWeightsX = 0;
                    int sumWeightsY = 0;

                    sumWeightsX = x.Value.Sum();
                    sumWeightsY = y.Value.Sum();

                    int retval = sumWeightsX.CompareTo(sumWeightsY);

                    return retval;
                }
            }
        }
    }
}
