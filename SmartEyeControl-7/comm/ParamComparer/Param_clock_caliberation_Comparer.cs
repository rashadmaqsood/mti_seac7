using System.Collections.Generic;
using System;

namespace comm
{
    public class Param_clock_caliberation_Comparer : EqualityComparer<Param_clock_caliberation>
    {
        public override bool Equals(Param_clock_caliberation x, Param_clock_caliberation y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("Param_clock_caliberation Param_ObjA");

                //Compare Begin_Date
                isMatch = DateTime.Equals(x.Begin_Date, y.Begin_Date);
                if (!isMatch)
                    return isMatch;
                //Compare End_Date
                isMatch = DateTime.Equals(x.End_Date, y.End_Date);
                if (!isMatch)
                    return isMatch;
                //Compare Enable_Day_Light_Saving_FLAG
                isMatch = x.Enable_Day_Light_Saving_FLAG == y.Enable_Day_Light_Saving_FLAG;
                if (!isMatch)
                    return isMatch;
                //Compare DayLight_Saving_Deviation
                isMatch = x.DayLight_Saving_Deviation == y.DayLight_Saving_Deviation;
                if (!isMatch)
                    return isMatch;
                //Compare Clock_Caliberation_PPM
                isMatch = x.Clock_Caliberation_PPM == y.Clock_Caliberation_PPM;
                if (!isMatch)
                    return isMatch;
                //Compare Enable_Caliberation_FLAG
                isMatch = x.Enable_Caliberation_FLAG == y.Enable_Caliberation_FLAG;
                if (!isMatch)
                    return isMatch;
                //Compare PPM_Add_FLAG
                isMatch = x.PPM_Add_FLAG == y.PPM_Add_FLAG;
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

        public override int GetHashCode(Param_clock_caliberation obj)
        {
            return obj.GetHashCode();
        }
    }

    public class Param_clock_caliberation_LimitedComparer : EqualityComparer<Param_clock_caliberation>
    {
        public override bool Equals(Param_clock_caliberation x, Param_clock_caliberation y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("Param_clock_caliberation Param_ObjA");

                //Compare Begin_Date
                //isMatch = DateTime.Equals(x.Begin_Date, y.Begin_Date);
                //if (!isMatch)
                //    return isMatch;
                //Compare End_Date
                //isMatch = DateTime.Equals(x.End_Date, y.End_Date);
                //if (!isMatch)
                //    return isMatch;
                //Compare Enable_Day_Light_Saving_FLAG
                //isMatch = x.Enable_Day_Light_Saving_FLAG == y.Enable_Day_Light_Saving_FLAG;
                //if (!isMatch)
                //    return isMatch;
                //Compare DayLight_Saving_Deviation
                //isMatch = x.DayLight_Saving_Deviation == y.DayLight_Saving_Deviation;
                //if (!isMatch)
                //    return isMatch;
                //Compare Clock_Caliberation_PPM
                isMatch = x.Clock_Caliberation_PPM == y.Clock_Caliberation_PPM;
                if (!isMatch)
                    return isMatch;
                //Compare Enable_Caliberation_FLAG
                isMatch = x.Enable_Caliberation_FLAG == y.Enable_Caliberation_FLAG;
                if (!isMatch)
                    return isMatch;
                //Compare PPM_Add_FLAG
                isMatch = x.PPM_Add_FLAG == y.PPM_Add_FLAG;
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

        public override int GetHashCode(Param_clock_caliberation obj)
        {
            return obj.GetHashCode();
        }
    }
}
