using System;
using System.Collections.Generic;
using DLMS.Comm;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;

namespace comm
{
    public class Param_TimeBaseEvents_Comparer : EqualityComparer<Param_TimeBaseEvents>
    {
        public override bool Equals(Param_TimeBaseEvents x, Param_TimeBaseEvents y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("Param_TimeBaseEvents Param_ObjA");

                //Compare Control_Enum
                isMatch = x.Control_Enum == y.Control_Enum;
                if (!isMatch)
                    return isMatch;
                //Compare Interval
                isMatch = x.Interval == y.Interval;
                if (!isMatch)
                    return isMatch;
                x.DateTime.Kind = y.DateTime.Kind = StDateTime.DateTimeType.DateTime;
                //Compare DateTime
                isMatch = new StDateTime_ContentComparer() {ClockStatus_Compare = false,UTCOffSet_Compare = false,Hundred_Compare =false}.Equals(x.DateTime, y.DateTime);

                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode(Param_TimeBaseEvents obj)
        {
            return obj.GetHashCode();
        }
    }
}
