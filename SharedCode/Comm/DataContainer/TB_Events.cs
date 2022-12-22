using SharedCode.Comm.HelperClasses;
using System;

namespace SharedCode.Comm.DataContainer
{
    public class TB_Events
    {
        public short Control = -1;
        public DateTimeChooser DT_Chooser = null;
        public TimeSpan Interval_TimeSpan = TimeSpan.Zero;
        public ushort Interval_Sink_Minutes = 0;
        public ushort Interval_Sink_Seconds = 0;
        public ushort Interval_Fixed_Minutes = 0;
        public ushort Interval_Fixed_Seconds = 0;

        public TB_Events()
        {
            DT_Chooser = new DateTimeChooser();
        }
    }
}
