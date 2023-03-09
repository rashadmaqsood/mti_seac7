using DLMS;
using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedCode.Comm.DataContainer
{
    public class Cumulative_billing_data : IDecodeAnyObject
    {
        public string msn;
        //public string reference_no;
        public DateTime date;
        public DateTime? resetDate;
        public double activeEnergy_T1;
        public double activeEnergy_T2;
        public double activeEnergy_T3;
        public double activeEnergy_T4;
        public double activeEnergy_TL;

        public double reactiveEnergy_T1;
        public double reactiveEnergy_T2;
        public double reactiveEnergy_T3;
        public double reactiveEnergy_T4;
        public double reactiveEnergy_TL;

        public double activeMDI_T1;
        public double activeMDI_T2;
        public double activeMDI_T3;
        public double activeMDI_T4;
        public double activeMDI_TL;
        public StringBuilder DBColumns = new StringBuilder();
        public StringBuilder DBValues = new StringBuilder();
        public StringBuilder DB_Columns_Values = new StringBuilder();

        public Cumulative_billing_data()
        {
            activeEnergy_T1 =
            activeEnergy_T2 =
            activeEnergy_T3 =
            activeEnergy_T4 =
            activeEnergy_TL =

            reactiveEnergy_T1 =
            reactiveEnergy_T2 =
            reactiveEnergy_T3 =
            reactiveEnergy_T4 =
            reactiveEnergy_TL =

            activeMDI_T1 =
            activeMDI_T2 =
            activeMDI_T3 =
            activeMDI_T4 =
            activeMDI_TL = Double.NaN;
        }
        ~Cumulative_billing_data()
        {

        }

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

    public class cumulativeBilling_SinglePhase:IDecodeAnyObject
    {
        public string msn;
        //public string reference_no;
        public DateTime date;
        public double activeEnergy;
        public double activeMDI;
        #region IDecodable Members
        public double Decode_Any(Base_Class arg, byte Class_ID)
        {
            return DLMS_Common.Decode_Any(arg, Class_ID);
        }

        public double Decode_Any(Base_Class arg, byte Class_ID, ref StDateTime TimeStamp)
        {
            throw new NotImplementedException();
        }

        public bool TryDecode_Any(Base_Class arg, byte Class_ID, IDecodeAnyObject DataContainer_Class_obj, string Data_Property)
        {
            return DLMS_Common.TryDecode_Any(arg, Class_ID, DataContainer_Class_obj, Data_Property);
        }

        public bool TryDecode_Any(Base_Class arg, byte Class_ID, IDecodeAnyObject DataContainer_Class_obj, string Data_Property, string CaptureTimeStamp_DataProperty)
        {
            throw new NotImplementedException();
        }

        public string Decode_Any_string(Base_Class arg, byte Class_ID)
        {
            throw new NotImplementedException();
        }

        public byte[] Decode_Any_ByteArray(Base_Class arg, byte Class_ID)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
