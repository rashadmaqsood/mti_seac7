using System;

namespace SharedCode.Comm.HelperClasses
{
    public class TL_LoadProfile
    {
        public CB_Day_Recoord_Tarrification TL_Record;
        public ushort counter;
        public ulong MDI_thatDay_kw;
        public ulong MDI_thatDay_kVAr;
        public DateTime datetime;
        public ulong CRC16;

        public byte[] RAWBYTES = new byte[500];

        public void DECODE_Any()
        {
            TL_Record = new CB_Day_Recoord_Tarrification(RAWBYTES, 0);
            counter = BitConverter.ToUInt16(new byte[] { RAWBYTES[46], RAWBYTES[47] }, 0);
            MDI_thatDay_kw = BitConverter.ToUInt32(new byte[] { RAWBYTES[48],RAWBYTES[49],RAWBYTES[50],RAWBYTES[51] }, 0);
            MDI_thatDay_kVAr = BitConverter.ToUInt32(new byte[] { RAWBYTES[52], RAWBYTES[53], RAWBYTES[54], RAWBYTES[55] }, 0);
            datetime = new DateTime(RAWBYTES[56] + 2000, RAWBYTES[57] & 0x0f, RAWBYTES[58], RAWBYTES[59], RAWBYTES[60], RAWBYTES[61]); 
        }

    }
}
