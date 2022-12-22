using System;
using DLMS;
using DLMS.Comm;
using SharedCode.Comm.Param;

namespace SharedCode.Comm.DataContainer
{
    public class Monthly_Billing : IDecodeAnyObject, IParam
    {
        public double KwhImport;
        public double KwhExport;
        public double KwhAbsolute;
        public double KvarhQ1;
        public double KvarhQ2;
        public double KvarhQ3;
        public double KvarhQ4;
        public double KvarhAbsolute;
        public double Kvah;
        public double TamperKwh;
        public double MdiKw;
        public double MdiKvar;
        public double PowerFactor;
        public double ThisMonthMdiKw;
        public double ThisMonthMdiKvar;

        #region Decode_Any Members

        public double Decode_Any(Base_Class arg, byte Class_ID)
        {
            return DLMS_Common.Decode_Any(arg, Class_ID);
        }

        public double Decode_Any(Base_Class arg, byte Class_ID, ref StDateTime TimeStamp)
        {
            return DLMS_Common.Decode_Any(arg, Class_ID, ref TimeStamp);
        }

        public string Decode_Any_string(Base_Class arg, byte Class_ID)
        {
            return DLMS_Common.Decode_Any_string(arg, Class_ID);
        }

        public byte[] Decode_Any_ByteArray(Base_Class arg, byte Class_ID)
        {
            return DLMS_Common.Decode_Any_ByteArray(arg, Class_ID);
        }

        public bool TryDecode_Any(Base_Class arg, byte Class_ID, IDecodeAnyObject DataContainer_Class_obj, string Data_Property)
        {
            return DLMS_Common.TryDecode_Any(arg, Class_ID, DataContainer_Class_obj, Data_Property);
        }

        public bool TryDecode_Any(Base_Class arg, byte Class_ID, IDecodeAnyObject DataContainer_Class_obj, string Data_Property, string CaptureTimeStamp_DataProperty)
        {
            return DLMS_Common.TryDecode_Any(arg, Class_ID, DataContainer_Class_obj, Data_Property, CaptureTimeStamp_DataProperty);
        }

        #endregion

    }
    public class Cumulative_Billing : IDecodeAnyObject
    {
        public double KwhImport;
        public double KwhExport;
        public double KwhAbsolute;
        public double KvarhQ1;
        public double KvarhQ2;
        public double KvarhQ3;
        public double KvarhQ4;
        public double KvarhAbsolute;
        public double Kvah;
        public double TamperKwh;
        public double MdiKw;
        public double MdiKvar;
        public double PowerFactor;
        public double CurrentMonthMdiKw;
        public double CurrentMonthMdiKvar;

        #region Decode_Any Members

        public double Decode_Any(Base_Class arg, byte Class_ID)
        {
            return DLMS_Common.Decode_Any(arg, Class_ID);
        }

        public double Decode_Any(Base_Class arg, byte Class_ID, ref StDateTime TimeStamp)
        {
            return DLMS_Common.Decode_Any(arg, Class_ID, ref TimeStamp);
        }

        public string Decode_Any_string(Base_Class arg, byte Class_ID)
        {
            return DLMS_Common.Decode_Any_string(arg, Class_ID);
        }

        public byte[] Decode_Any_ByteArray(Base_Class arg, byte Class_ID)
        {
            return DLMS_Common.Decode_Any_ByteArray(arg, Class_ID);
        }

        public bool TryDecode_Any(Base_Class arg, byte Class_ID, IDecodeAnyObject DataContainer_Class_obj, string Data_Property)
        {
            return DLMS_Common.TryDecode_Any(arg, Class_ID, DataContainer_Class_obj, Data_Property);
        }

        public bool TryDecode_Any(Base_Class arg, byte Class_ID, IDecodeAnyObject DataContainer_Class_obj, string Data_Property, string CaptureTimeStamp_DataProperty)
        {
            return DLMS_Common.TryDecode_Any(arg, Class_ID, DataContainer_Class_obj, Data_Property, CaptureTimeStamp_DataProperty);
        }

        #endregion
    }
}
