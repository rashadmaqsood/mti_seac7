using System;
using System.Collections.Generic;
using DLMS.Comm;

namespace comm
{
    public class StDateTime_ContentComparer : EqualityComparer<StDateTime>
    {
        public bool ClockStatus_Compare { get; set; }
        public bool UTCOffSet_Compare { get; set; }
        public bool Hundred_Compare { get; set; }

        public override bool Equals(StDateTime x, StDateTime y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("StDateTime Argument");
                else if (x == y)
                {
                    isMatch = true;
                    return true;
                }
                //match StDateTime Kind DateTime,Date,Time
                if (x.Kind != y.Kind)
                {
                    isMatch = false;
                }
                else
                    isMatch = true;
                if (!isMatch)
                    return isMatch;
                #region Kind DateTime
                //match StDatTime Kind DateTime
                if (x.Kind == StDateTime.DateTimeType.DateTime)
                {
                    //Compare UTC_OffSet
                    if (UTCOffSet_Compare)
                    {
                        isMatch = x.UTCOffset == y.UTCOffset;
                        if (!isMatch)
                            return isMatch;
                    }
                    //Compare Status
                    if (ClockStatus_Compare)
                    {
                        isMatch = x.ClockStatus == y.ClockStatus;
                        if (!isMatch)
                            return isMatch;
                    }
                }
                #endregion
                #region Compare Date
                if (x.Kind == StDateTime.DateTimeType.Date || x.Kind == StDateTime.DateTimeType.DateTime)
                {
                    //Compare Year
                    isMatch = x.Year == y.Year;
                    if (!isMatch)
                        return isMatch;
                    //Compare Month
                    isMatch = x.Month == y.Month;
                    if (!isMatch)
                        return isMatch;
                    //Compare DayOfMonth
                    isMatch = x.DayOfMonth == y.DayOfMonth;
                    if (!isMatch)
                        return isMatch;
                    //Compare DayOfWeek
                    isMatch = x.DayOfWeek == y.DayOfWeek;
                    if (!isMatch)
                        return isMatch;
                }
                #endregion
                #region Compare Time
                if (x.Kind == StDateTime.DateTimeType.Time || x.Kind == StDateTime.DateTimeType.DateTime)
                {
                    //Compare Hour
                    isMatch = x.Hour == y.Hour;
                    if (!isMatch)
                        return isMatch;
                    //Compare Minute
                    isMatch = x.Minute == y.Minute;
                    if (!isMatch)
                        return isMatch;
                    //Compare Second
                    isMatch = x.Second == y.Second;
                    if (!isMatch)
                        return isMatch;
                    //Compare Hundred
                    if (Hundred_Compare)
                    {
                        isMatch = x.Hundred == y.Hundred;
                        if (!isMatch)
                            return isMatch;
                    }
                }
                #endregion
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
            return isMatch;
        }

        public override int GetHashCode(StDateTime obj)
        {
            //TODO:Later Change
            return obj.GetHashCode();
        }
    }
}
