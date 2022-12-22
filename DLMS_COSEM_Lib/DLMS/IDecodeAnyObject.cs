using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLMS.Comm;

namespace DLMS
{
    public interface IDecodeAnyObject
    {
        double Decode_Any(Base_Class arg, byte Class_ID);
        double Decode_Any(Base_Class arg, byte Class_ID, ref StDateTime TimeStamp);

        bool TryDecode_Any(Base_Class arg, byte Class_ID, IDecodeAnyObject DataContainer_Class_obj, string Data_Property);
        bool TryDecode_Any(Base_Class arg, byte Class_ID, IDecodeAnyObject DataContainer_Class_obj, string Data_Property, string CaptureTimeStamp_DataProperty);

        string Decode_Any_string(Base_Class arg, byte Class_ID);
        byte[] Decode_Any_ByteArray(Base_Class arg, byte Class_ID);
    }
}
