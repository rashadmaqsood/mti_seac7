using System;
using System.Collections.Generic;

namespace SmartEyeControl_7.comm
{
    public class TBE_PowerFail_Comparer : EqualityComparer<TBE_PowerFail>
    {
        public override bool Equals(TBE_PowerFail x, TBE_PowerFail y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("TBE_PowerFail Param_ObjA");

                //Compare disableEventAtPowerFail_TBE1
                isMatch = x.disableEventAtPowerFail_TBE1 == y.disableEventAtPowerFail_TBE1;
                if (!isMatch)
                    return isMatch;
                //Compare disableEventAtPowerFail_TBE2
                isMatch = x.disableEventAtPowerFail_TBE2 == y.disableEventAtPowerFail_TBE2;

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

        public override int GetHashCode(TBE_PowerFail obj)
        {
            return obj.GetHashCode();
        }
    }
}
