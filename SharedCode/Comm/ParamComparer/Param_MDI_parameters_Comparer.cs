using DLMS.Comm;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;

namespace comm
{
    public class Param_MDI_parameters_Comparer : EqualityComparer<Param_MDI_parameters>
    {
        public override bool Equals(Param_MDI_parameters x, Param_MDI_parameters y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null || x.Auto_reset_date == null || y.Auto_reset_date == null)
                    throw new ArgumentNullException("Param_MDI_parameters Param_ObjA");
                y.Auto_reset_date.Kind = x.Auto_reset_date.Kind = StDateTime.DateTimeType.DateTime;
                //Compare Auto_reset_date
                isMatch = new StDateTime_ContentComparer().Equals(x.Auto_reset_date, y.Auto_reset_date);
                if (!isMatch)
                    return isMatch;
                //Compare Minimum_Time_Interval
                isMatch = x.Minimum_Time_Interval_Between_Resets_In_case_of_Manual_Reset == y.Minimum_Time_Interval_Between_Resets_In_case_of_Manual_Reset;
                if (!isMatch)
                    return isMatch;
                //Compare Min_Time_Unit
                isMatch = x.Min_Time_Unit == y.Min_Time_Unit;
                if (!isMatch)
                    return isMatch;
                //Compare MDI_Interval
                isMatch = x.MDI_Interval == y.MDI_Interval;
                if (!isMatch)
                    return isMatch;
                //Compare Roll_slide_count
                isMatch = x.Roll_slide_count == y.Roll_slide_count;
                if (!isMatch)
                    return isMatch;
                //Compare Roll_slide_count
                isMatch = x.FLAG_Auto_Reset_in_PowerDown_Mode == y.FLAG_Auto_Reset_in_PowerDown_Mode;
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

        public override int GetHashCode(Param_MDI_parameters obj)
        {
            return obj.GetHashCode();
        }
    }
}
