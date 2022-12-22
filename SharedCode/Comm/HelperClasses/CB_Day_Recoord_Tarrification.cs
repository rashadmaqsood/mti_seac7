using System;

namespace SharedCode.Comm.HelperClasses
{
    public class CB_Day_Recoord_Tarrification
    {
        public ulong KWH_P;
        public ulong KWH_N;
        public ulong[] KVarh = new ulong[4];
        public ulong Kvah;
        public ulong Tamper;
        public ulong MDI_kw;
        public ulong MDI_kvar;
        public byte pf;
        public DateTime MDI_CaptureTIme ;
        public ushort CRC16;
        

        public CB_Day_Recoord_Tarrification(byte[] RAWBYTES, int start_index)
        {
            byte[] bytes = new byte[] { RAWBYTES[start_index + 0], RAWBYTES[start_index + 1], RAWBYTES[start_index + 2], RAWBYTES[start_index + 3] };
            KWH_P = BitConverter.ToUInt32(bytes, 0);
            bytes = new byte[] { RAWBYTES[start_index + 4], RAWBYTES[start_index + 5], RAWBYTES[start_index + 6], RAWBYTES[start_index + 7] };
            KWH_N = BitConverter.ToUInt32(bytes, 0);
            bytes = new byte[] { RAWBYTES[start_index + 8], RAWBYTES[start_index + 9], RAWBYTES[start_index + 10], RAWBYTES[start_index + 11] };
            KVarh[0] = BitConverter.ToUInt32(bytes, 0);
            bytes = new byte[] { RAWBYTES[start_index + 12], RAWBYTES[start_index + 13], RAWBYTES[start_index + 14], RAWBYTES[start_index + 15] };
            KVarh[1] = BitConverter.ToUInt32(bytes, 0);
            bytes = new byte[] { RAWBYTES[start_index + 16], RAWBYTES[start_index + 17], RAWBYTES[start_index + 18], RAWBYTES[start_index + 19] };
            KVarh[2] = BitConverter.ToUInt32(bytes, 0);
            bytes = new byte[] { RAWBYTES[start_index + 20], RAWBYTES[start_index + 21], RAWBYTES[start_index + 22], RAWBYTES[start_index + 23] };
            KVarh[3] = BitConverter.ToUInt32(bytes, 0);
            bytes = new byte[] { RAWBYTES[start_index + 24], RAWBYTES[start_index + 25], RAWBYTES[start_index + 26], RAWBYTES[start_index + 27] };
            Kvah = BitConverter.ToUInt32(bytes, 0);
            bytes = new byte[] { RAWBYTES[start_index + 28], RAWBYTES[start_index + 29], RAWBYTES[start_index + 30], RAWBYTES[start_index + 31] };
            Tamper = BitConverter.ToUInt32(bytes, 0);
            bytes = new byte[] { RAWBYTES[start_index + 32], RAWBYTES[start_index + 33], RAWBYTES[start_index + 34], RAWBYTES[start_index + 35] };
            MDI_kw = BitConverter.ToUInt32(bytes, 0);
            bytes = new byte[] { RAWBYTES[start_index + 36], RAWBYTES[start_index + 37], RAWBYTES[start_index + 38], RAWBYTES[start_index + 39] };
            MDI_kvar = BitConverter.ToUInt32(bytes, 0);

            pf = RAWBYTES[start_index + 40] ;
            int year = RAWBYTES[start_index + 41] + 2000;
            int month = RAWBYTES[start_index + 42] & 0x0f;
            int date = RAWBYTES[start_index + 43];
            int hour = RAWBYTES[start_index + 44];
            int minnut = RAWBYTES[start_index + 45];
            MDI_CaptureTIme = new DateTime(year, month, date, hour, minnut, 0);
            //bytes = new byte[] { RAWBYTES[start_index + 46], RAWBYTES[start_index + 47] };
            //CRC16 = BitConverter.ToUInt16(bytes, 0);
        }
    }
}
