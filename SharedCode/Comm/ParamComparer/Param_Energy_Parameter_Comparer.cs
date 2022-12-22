using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_Energy_Parameter_Comparer : EqualityComparer<Param_Energy_Parameter>
    {
        public override bool Equals(Param_Energy_Parameter x, Param_Energy_Parameter y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("Param_Energy_Parameter Param_ObjA");

                //Compare EnergyQuadrant1
                isMatch = x.Quad1 == y.Quad1;
                if (!isMatch)
                    return isMatch;
                //Compare EnergyQuadrant2
                isMatch = x.Quad2 == y.Quad2;
                if (!isMatch)
                    return isMatch;
                //Compare EnergyQuadrant3
                isMatch = x.Quad3 == y.Quad3;
                if (!isMatch)
                    return isMatch;
                //Compare EnergyQuadrant4
                isMatch = x.Quad4 == y.Quad4;
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

        public override int GetHashCode(Param_Energy_Parameter obj)
        {
            return obj.GetHashCode();
        }
    }
}
