using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLMS.Comm
{
    public interface ICustomStructure : ICloneable
    {
        byte[] Encode_Data();
        void Decode_Data(byte[] Data);
        void Decode_Data(byte[] Data, ref int array_traverse, int length);
    }
}
