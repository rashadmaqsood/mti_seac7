using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using comm;
using DLMS.Comm;

namespace AccurateOptocomSoftware.comm.Param
{
    public class Param_ActivityCalendar_Comparer : EqualityComparer<Param_ActivityCalendar>
    {
        public Param_ActivityCalendar_Comparer()
        {
            Compare_ActivationDateTime = true;
            Compare_Calendar_Title = true;
        }

        public bool Compare_ActivationDateTime { get; set; }
        public bool Compare_Calendar_Title { get; set; }

        public override bool Equals(Param_ActivityCalendar Obj_A, Param_ActivityCalendar Obj_B)
        {
            bool isMatch = false;
            try
            {
                if (Obj_A == null || Obj_B == null)
                    throw new ArgumentNullException("Param_ActivityCalendar ObjA");
                //Reference Match
                if (Obj_A == Obj_B)
                {
                    isMatch = true;
                    return isMatch;
                }
                if (Compare_Calendar_Title)
                {
                    //Compare CalendarName
                    isMatch = String.Equals(Obj_A.CalendarName, Obj_B.CalendarName);
                    if (!isMatch)
                        return isMatch;
                    //Compare CalendarNamePassive
                    //isMatch = String.Equals(Obj_A.CalendarNamePassive, Obj_B.CalendarNamePassive);
                    //if (!isMatch)
                    //    return isMatch;
                }
                if (Compare_ActivationDateTime)
                {
                    //Compare CalendarstartDate
                    isMatch = String.Equals(Obj_A.CalendarstartDate, Obj_B.CalendarstartDate);
                    if (!isMatch)
                        return isMatch;
                }

                //Compare ParamDayProfile
                ParamDayProfile_Comparer Param_DP_Comparer = new ParamDayProfile_Comparer();
                isMatch = Param_DP_Comparer.Equals(Obj_A.ParamDayProfile, Obj_B.ParamDayProfile);
                if (!isMatch)
                    return isMatch;
                //Compare ParamWeekProfile
                ParamWeekProfile_Comparer Param_WK_Comparer = new ParamWeekProfile_Comparer();
                isMatch = Param_WK_Comparer.Equals(Obj_A.ParamWeekProfile, Obj_B.ParamWeekProfile);
                if (!isMatch)
                    return isMatch;
                //Param_SeasonProfile
                Param_SeasonProfile_Comparer Param_SeasP_Comparer = new Param_SeasonProfile_Comparer();
                isMatch = Param_SeasP_Comparer.Equals(Obj_A.ParamSeasonProfile, Obj_B.ParamSeasonProfile);
                if (!isMatch)
                    return isMatch;
                //Param_SpecialDayProfile
                Param_SpecialDayProfile_Comparer Param_SP_Comparer = new Param_SpecialDayProfile_Comparer();
                isMatch = Param_SP_Comparer.Equals(Obj_A.ParamSpecialDay, Obj_B.ParamSpecialDay);

                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                isMatch = false;
            }
            return isMatch;
        }

        public override int GetHashCode(Param_ActivityCalendar obj)
        {
            return obj.GetHashCode();
        }
    }

    public class Param_SpecialDayProfile_Comparer : EqualityComparer<Param_SpecialDay>
    {
        public override bool Equals(Param_SpecialDay Obj_A, Param_SpecialDay Obj_B)
        {
            bool isMatch = false;
            try
            {
                if (Obj_A == null || Obj_B == null)
                    throw new ArgumentNullException("Param_SpecialDay ObjA");
                //Reference Match
                if (Obj_A == Obj_B)
                {
                    isMatch = true;
                    return isMatch;
                }
                //Compare SeasonProfile_Table Count
                isMatch = Obj_A.specialDay_Table.Count == Obj_B.specialDay_Table.Count;
                if (!isMatch)
                    return isMatch;
                //Compare SeasonProfile
                SpecialDay SpecialDay_ObjA = null, SpecialDay_ObjB = null;
                SpecialDay_Comparer SpecialDayProf_Comparer = new SpecialDay_Comparer();

                for (ushort index = 0; index < Obj_A.specialDay_Table.Count; index++)
                {
                    SpecialDay_ObjA = Obj_A.specialDay_Table[index];
                    SpecialDay_ObjB = Obj_B.specialDay_Table[index];

                    isMatch = SpecialDayProf_Comparer.Equals(SpecialDay_ObjA, SpecialDay_ObjB);
                    if (!isMatch)
                        return isMatch;
                }
                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                isMatch = false;
            }
            return isMatch;
        }

        public override int GetHashCode(Param_SpecialDay obj)
        {
            return obj.GetHashCode();
        }
    }

    public class SpecialDay_Comparer : EqualityComparer<SpecialDay>
    {
        public override bool Equals(SpecialDay Obj_A, SpecialDay Obj_B)
        {
            bool isMatch = false;
            try
            {
                if (Obj_A == null || Obj_B == null)
                    throw new ArgumentNullException("SeasonProfile ObjA");
                //Reference Match
                if (Obj_A == Obj_B)
                {
                    isMatch = true;
                    return isMatch;
                }
                //Compare SpecialDayID Name
                isMatch = String.Equals(Obj_A.SpecialDayID, Obj_B.SpecialDayID);
                if (!isMatch)
                    return isMatch;
                //Compare SpecialDay StartDateTime
                StDateTime StartDate_ObjA = new StDateTime(Obj_A.StartDate) { Kind = StDateTime.DateTimeType.Date };
                StDateTime StartDate_ObjB = new StDateTime(Obj_B.StartDate) { Kind = StDateTime.DateTimeType.Date };
                isMatch = new StDateTime_ContentComparer() { ClockStatus_Compare = false, UTCOffSet_Compare = false }.Equals(StartDate_ObjA, StartDate_ObjB);
                if (!isMatch)
                    return isMatch;
                //Compare SpecialDay DayProfile
                isMatch = new DayProfile_Comparer().Equals(Obj_A.DayProfile, Obj_B.DayProfile);

                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                isMatch = false;
            }
            return isMatch;
        }

        public override int GetHashCode(SpecialDay obj)
        {
            return obj.GetHashCode();
        }
    }

    public class Param_SeasonProfile_Comparer : EqualityComparer<Param_SeasonProfile>
    {
        public override bool Equals(Param_SeasonProfile Obj_A, Param_SeasonProfile Obj_B)
        {
            bool isMatch = false;
            try
            {
                if (Obj_A == null || Obj_B == null)
                    throw new ArgumentNullException("Param_SeasonProfile ObjA");
                //Reference Match
                if (Obj_A == Obj_B)
                {
                    isMatch = true;
                    return isMatch;
                }
                //Compare SeasonProfile_Table Count
                isMatch = Obj_A.seasonProfile_Table.Count == Obj_B.seasonProfile_Table.Count;
                if (!isMatch)
                    return isMatch;
                //Compare SeasonProfile
                SeasonProfile Season_ObjA = null, Season_ObjB = null;
                SeasonProfile_Comparer SeasonProf_Comparer = new SeasonProfile_Comparer();

                for (ushort index = 1; index <= Obj_A.seasonProfile_Table.Count; index++)
                {
                    Season_ObjA = Obj_A.Get_Season_Profile(index);
                    Season_ObjB = Obj_B.Get_Season_Profile(index);

                    isMatch = SeasonProf_Comparer.Equals(Season_ObjA, Season_ObjB);
                    if (!isMatch)
                        return isMatch;
                }
                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                isMatch = false;
            }
            return isMatch;
        }

        public override int GetHashCode(Param_SeasonProfile obj)
        {
            return obj.GetHashCode();
        }
    }

    public class SeasonProfile_Comparer : EqualityComparer<SeasonProfile>
    {
        public override bool Equals(SeasonProfile Obj_A, SeasonProfile Obj_B)
        {
            bool isMatch = false;
            try
            {
                if (Obj_A == null || Obj_B == null)
                    throw new ArgumentNullException("SeasonProfile ObjA");
                //Reference Match
                if (Obj_A == Obj_B)
                {
                    isMatch = true;
                    return isMatch;
                }
                //Compare SeasonProfile Name
                isMatch = String.Equals(Obj_A.Profile_Name, Obj_B.Profile_Name);
                if (!isMatch)
                    return isMatch;
                //Compare SeasonProfile Date
                Obj_A.Start_Date.Kind = Obj_B.Start_Date.Kind = StDateTime.DateTimeType.Date;

                StDateTime StartDate_ObjA = new StDateTime(Obj_A.Start_Date) { Kind = StDateTime.DateTimeType.Date };
                StDateTime StartDate_ObjB = new StDateTime(Obj_B.Start_Date) { Kind = StDateTime.DateTimeType.Date };
                isMatch = new StDateTime_ContentComparer() { ClockStatus_Compare = false, UTCOffSet_Compare = false }.Equals(StartDate_ObjA, StartDate_ObjB);
                if (!isMatch)
                    return isMatch;
                //Compare WeekProfile
                isMatch = new WeekProfile_Comparer().Equals(Obj_A.Week_Profile, Obj_B.Week_Profile);
                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                isMatch = false;
            }
            return isMatch;
        }

        public override int GetHashCode(SeasonProfile obj)
        {
            return obj.GetHashCode();
        }
    }

    public class ParamWeekProfile_Comparer : EqualityComparer<Param_WeeKProfile>
    {
        public override bool Equals(Param_WeeKProfile x, Param_WeeKProfile y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || x.weekProfile_Table == null || y == null || y.weekProfile_Table == null)
                    throw new ArgumentNullException("Param_WeeKProfile ObjA");
                //Reference Match
                if (x == y)
                {
                    isMatch = true;
                    return isMatch;
                }
                //Compare WeekProfile_Table Count
                isMatch = x.weekProfile_Table.Count == y.weekProfile_Table.Count;
                if (!isMatch)
                    return isMatch;

                WeekProfile_Comparer WkInstance_Comparer = new WeekProfile_Comparer();
                WeekProfile Obj_A = null, Obj_B = null;
                //Compare WeekProfile
                for (byte WK_Index = 0; WK_Index < x.weekProfile_Table.Count; WK_Index++)
                {
                    Obj_A = x.weekProfile_Table[WK_Index];
                    Obj_B = y.weekProfile_Table[WK_Index];
                    isMatch = WkInstance_Comparer.Equals(Obj_A, Obj_B);
                    if (!isMatch)
                        break;
                }
                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                isMatch = false;
            }
            return isMatch;
        }

        public override int GetHashCode(Param_WeeKProfile obj)
        {
            return obj.GetHashCode();
        }
    }

    public class WeekProfile_Comparer : EqualityComparer<WeekProfile>
    {
        public override bool Equals(WeekProfile Obj_A, WeekProfile Obj_B)
        {
            bool isMatch = false;
            try
            {
                if (Obj_A == null || Obj_B == null)
                    throw new ArgumentNullException("WeekProfile ObjA");
                //Reference Match
                if (Obj_A == Obj_B)
                {
                    isMatch = true;
                    return isMatch;
                }
                //Compare WeekProfile Name
                isMatch = String.Equals(Obj_A.Week_Profile_Name, Obj_B.Week_Profile_Name);
                if (!isMatch)
                    return isMatch;
                //Compare DayProfile Ids
                //Compare DayProfile Monday
                isMatch = Obj_A.Day_Profile_MON.Day_Profile_ID == Obj_B.Day_Profile_MON.Day_Profile_ID;
                if (!isMatch)
                    return isMatch;
                //Compare DayProfile Tuesday
                isMatch = Obj_A.Day_Profile_TUE.Day_Profile_ID == Obj_B.Day_Profile_TUE.Day_Profile_ID;
                if (!isMatch)
                    return isMatch;
                //Compare DayProfile Wednesday
                isMatch = Obj_A.Day_Profile_WED.Day_Profile_ID == Obj_B.Day_Profile_WED.Day_Profile_ID;
                if (!isMatch)
                    return isMatch;
                //Compare DayProfile Thursday
                isMatch = Obj_A.Day_Profile_THRU.Day_Profile_ID == Obj_B.Day_Profile_THRU.Day_Profile_ID;
                if (!isMatch)
                    return isMatch;
                //Compare DayProfile Friday
                isMatch = Obj_A.Day_Profile_FRI.Day_Profile_ID == Obj_B.Day_Profile_FRI.Day_Profile_ID;
                if (!isMatch)
                    return isMatch;
                //Compare DayProfile Saturday
                isMatch = Obj_A.Day_Profile_SAT.Day_Profile_ID == Obj_B.Day_Profile_SAT.Day_Profile_ID;
                if (!isMatch)
                    return isMatch;
                //Compare DayProfile Sunday
                isMatch = Obj_A.Day_Profile_SUN.Day_Profile_ID == Obj_B.Day_Profile_SUN.Day_Profile_ID;
                if (!isMatch)
                    return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                isMatch = false;
            }
            return isMatch;
        }

        public override int GetHashCode(WeekProfile obj)
        {
            return obj.GetHashCode();
        }
    }

    public class ParamDayProfile_Comparer : EqualityComparer<Param_DayProfile>
    {
        public override bool Equals(Param_DayProfile x, Param_DayProfile y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("Param_DayProfile ObjA");
                if (x.DayProfileCount == -1 || x.DayProfileCount == -1)
                    throw new ArgumentNullException("Param_DayProfile ObjA");
                //Reference Match
                if (x == y)
                {
                    isMatch = true;
                    return isMatch;
                }
                //Compare DayProfile Count
                isMatch = x.DayProfileCount == y.DayProfileCount;
                if (!isMatch)
                    return isMatch;
                DayProfile_Comparer DProfile_Comparer = new DayProfile_Comparer();
                DayProfile Obj_A = null, Obj_B = null;
                //Compare DayProfiles
                for (byte DP_Id = 1; DP_Id <= x.DayProfileCount; DP_Id++)
                {
                    Obj_A = x.GetDayProfile(DP_Id);
                    Obj_B = y.GetDayProfile(DP_Id);

                    isMatch = DProfile_Comparer.Equals(Obj_A, Obj_B);
                    if (!isMatch)
                        break;
                }
                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                isMatch = false;
            }
            return isMatch;
        }

        public override int GetHashCode(Param_DayProfile obj)
        {
            return obj.GetHashCode();
        }
    }

    public class DayProfile_Comparer : EqualityComparer<DayProfile>
    {
        public override bool Equals(DayProfile Obj_A, DayProfile Obj_B)
        {
            bool isMatch = false;
            try
            {
                if (Obj_A == null || Obj_B == null)
                    throw new ArgumentNullException("DayProfile ObjA");
                //Reference Match
                if (Obj_A == Obj_B)
                {
                    isMatch = true;
                    return isMatch;
                }
                //Compare Day_Profile_ID
                isMatch = Obj_A.Day_Profile_ID == Obj_B.Day_Profile_ID;
                if (!isMatch)
                    return isMatch;
                //Compare TimeSlots
                if (Obj_A.dayProfile_Schedule == null || Obj_A.dayProfile_Schedule.Count <= 0 ||
                    Obj_B.dayProfile_Schedule == null || Obj_B.dayProfile_Schedule.Count <= 0)
                    throw new ArgumentNullException("Param_DayProfile_dayProfile_Schedule ObjA");
                //Compare TimeSlot_Count
                isMatch = Obj_A.dayProfile_Schedule.Count == Obj_B.dayProfile_Schedule.Count;
                if (!isMatch)
                    return isMatch;
                TimeSlot_Comparer TimeSlot_Comparer = new TimeSlot_Comparer();
                TimeSlot ObjA_Slot = null, ObjB_Slot = null;

                for (int intDP_Index = 0; intDP_Index < Obj_A.dayProfile_Schedule.Count; intDP_Index++)
                {
                    ObjA_Slot = Obj_A.dayProfile_Schedule[intDP_Index];
                    ObjB_Slot = Obj_B.dayProfile_Schedule[intDP_Index];
                    isMatch = TimeSlot_Comparer.Equals(ObjA_Slot, ObjB_Slot);
                    if (!isMatch)
                        break;
                }
                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                isMatch = false;
            }
            return isMatch;
        }

        public override int GetHashCode(DayProfile obj)
        {
            return obj.GetHashCode();
        }
    }

    public class TimeSlot_Comparer : EqualityComparer<TimeSlot>
    {
        public override bool Equals(TimeSlot x, TimeSlot y)
        {
            bool isMatch = false;
            try
            {
                if (x == null || y == null)
                    throw new ArgumentNullException("TimeSlot ObjA");
                //Reference Match
                if (x == y)
                {
                    isMatch = true;
                    return isMatch;
                }
                //Compare TimeSlotId
                isMatch = x.TimeSlotId == y.TimeSlotId;
                if (!isMatch)
                    return isMatch;
                //Compare StartTime
                isMatch = TimeSpan.Equals(x.StartTime, y.StartTime);
                if (!isMatch)
                    return isMatch;
                //Compare ScriptSelector
                isMatch = x.ScriptSelector == y.ScriptSelector;

                return isMatch;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                isMatch = false;
            }
            return isMatch;
        }

        public override int GetHashCode(TimeSlot obj)
        {
            return obj.GetHashCode();
        }
    }
}
