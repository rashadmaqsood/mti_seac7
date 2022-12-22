using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using comm;

namespace SmartEyeControl_7.comm
{
    public class Param_DisplayPowerDown_Comparer : EqualityComparer<Param_Display_PowerDown>
    {
        public override bool Equals(Param_Display_PowerDown x, Param_Display_PowerDown y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("Param_Display_PowerDown Param_ObjA");

                //Compare Flags
                isMatch = x.Flags == y.Flags;
                if (!isMatch)
                    return isMatch;
                //Compare OnTime
                isMatch = x.OnTime == y.OnTime;
                if (!isMatch)
                    return isMatch;
                //Compare OffTime
                isMatch = x.OffTime == y.OffTime;
                if (!isMatch)
                    return isMatch;
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

        public override int GetHashCode(Param_Display_PowerDown obj)
        {
            return obj.GetHashCode();
        }
    }
}
