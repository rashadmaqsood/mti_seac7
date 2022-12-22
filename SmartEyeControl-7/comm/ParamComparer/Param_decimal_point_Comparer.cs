using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_decimal_point_Comparer : EqualityComparer<Param_decimal_point>
    {
        public override bool Equals(Param_decimal_point x, Param_decimal_point y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("Param_decimal_point Param_ObjA");

                //Compare Billing_Energy
                isMatch = x.Billing_Energy == y.Billing_Energy;
                if (!isMatch)
                    return isMatch;
                //Compare Billing_MDI
                isMatch = x.Billing_MDI == y.Billing_MDI;
                if (!isMatch)
                    return isMatch;
                //Compare Instataneous_Voltage
                isMatch = x.Instataneous_Voltage == y.Instataneous_Voltage;
                if (!isMatch)
                    return isMatch;
                //Compare Instataneous_Current
                isMatch = x.Instataneous_Current == y.Instataneous_Current;
                if (!isMatch)
                    return isMatch;
                //Compare Instataneous_Power
                isMatch = x.Instataneous_Power == y.Instataneous_Power;
                if (!isMatch)
                    return isMatch;
                //Compare Instataneous_MDI
                isMatch = x.Instataneous_MDI == y.Instataneous_MDI;

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

        public override int GetHashCode(Param_decimal_point obj)
        {
            return obj.GetHashCode();
        }
    }
}
