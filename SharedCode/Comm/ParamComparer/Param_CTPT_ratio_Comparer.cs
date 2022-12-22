using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_CTPT_ratio_Comparer : EqualityComparer<Param_CTPT_Ratio>
    {
        public override bool Equals(Param_CTPT_Ratio x, Param_CTPT_Ratio y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("Param_CTPT_Ratio Param_ObjA");

                //Compare CTratio_Numerator
                isMatch = x.CTratio_Numerator == y.CTratio_Numerator;
                if (!isMatch)
                    return isMatch;
                //Compare CTratio_Denominator
                isMatch = x.CTratio_Denominator == y.CTratio_Denominator;
                if (!isMatch)
                    return isMatch;
                //Compare CTratio_Numerator
                isMatch = x.PTratio_Numerator == y.PTratio_Numerator;
                if (!isMatch)
                    return isMatch;
                //Compare PTratio_Denominator
                isMatch = x.PTratio_Denominator == y.PTratio_Denominator;

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

        public override int GetHashCode(Param_CTPT_Ratio obj)
        {
            return obj.GetHashCode();
        }
    }
}