using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedCode.Comm.Param
{
    class Param_SignalStrength : ICustomStructure,IParam
    {
        public int NetworkType { get; private set; }
        public int SignalsStrngth { get; private set; }
        public int SignalStrengthDb => SignalsStrngth - 256;

        #region ICUstome Structure Members
        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void Decode_Data(byte[] Data)
        {
            int array_traverse = 0;
            Decode_Data(Data, ref array_traverse, Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            if (Data.Length < array_traverse + 2)
                throw new Exception("Invalid Data Received.");
            NetworkType = Data[array_traverse++];
            SignalsStrngth = Data[array_traverse++];
        }

        public byte[] Encode_Data()
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
