using System.Collections.Generic;
using DLMS.Comm;

namespace SharedCode.Comm.DataContainer
{
    public class SaveInstantaneous
    {
        public StDateTime meterDate;
        public string msn;
        public List<double> values;

        public SaveInstantaneous()
        {
            meterDate = null;
            msn = "";
            values = new List<double>();
        }
    }
}
