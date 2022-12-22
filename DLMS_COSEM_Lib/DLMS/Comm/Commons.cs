using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLMS.Comm.Common
{
    public class Commons
    {
        public static readonly TimeSpan ReadLOCKLow_TimeOut = TimeSpan.FromSeconds(05);
        public static readonly TimeSpan ReadLOCK_TimeOut = TimeSpan.FromSeconds(10);
        public static readonly TimeSpan WriteLOCK_TimeOut = TimeSpan.FromSeconds(15);
        public static readonly TimeSpan WriteLOCKLow_TimeOut = TimeSpan.FromSeconds(05);
    }
}
