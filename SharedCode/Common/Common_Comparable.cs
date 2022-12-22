using SharedCode.Comm.Param;

namespace SharedCode.Common
{
    public static class Common_Comparable
    {
        public static int CompareTimeSlotById(TimeSlot x, TimeSlot y)
        {
            int retval = -1;
            if (x == null)
            {
                if (y == null)
                {
                    /// If x is null and y is null, they're
                    /// equal 
                    retval = 0;
                }
                else
                {
                    /// If x is null and y is not null, y
                    /// is greater 
                    retval =  -1;
                }
            }
            else
            {
                /// If x is not null...
                /// and y is null, x is greater
                if (y == null)
                {
                    retval =  1;
                }
                else
                {
                    // y is not null, compare the 
                    // lengths of the two strings
                    retval = x.TimeSlotId.CompareTo(y.TimeSlotId);
                }
            }
            return retval;
        }
    }
}
