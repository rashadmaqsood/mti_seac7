using System.Collections.Generic;
using DLMS.Comm;
using DLMS;
using SharedCode.Comm.DataContainer;

namespace SharedCode.Comm.HelperClasses
{
    public interface IBillingDataFormatter
    {
        StOBISCode GetOBISCode(Get_Index OBIS_Index);
        List<BillingData> MakeBillingData(List<ILValue[]> CommObjs);
        List<BillingData> MakeBillingData(List<ILValue[]> CommObjs, List<BillingItem> BillingFormatItems);
    }
}
