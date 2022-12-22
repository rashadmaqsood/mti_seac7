using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEyeControl_7.comm
{
    class CB_DayRecord
    {
        

        public byte[] RAWBYTES=new byte[500];
        public List<CB_Day_Recoord_Tarrification> tarrifs = new List<CB_Day_Recoord_Tarrification>();
        public DateTime LastResetDateTime { get; set; }
        public DateTime ThisDateTime { get; set; }
        public int ThisRecordCounter { get; set; }



        public void Decode_Any()
        {
            tarrifs.Add( new CB_Day_Recoord_Tarrification(RAWBYTES, 0));
            tarrifs.Add( new CB_Day_Recoord_Tarrification(RAWBYTES, 48));
            tarrifs.Add( new CB_Day_Recoord_Tarrification(RAWBYTES, 96));
            tarrifs.Add( new CB_Day_Recoord_Tarrification(RAWBYTES, 144));
            tarrifs.Add( new CB_Day_Recoord_Tarrification(RAWBYTES, 192));

            LastResetDateTime = Decode_Date( RAWBYTES, 240);
            ThisDateTime = Decode_Date( RAWBYTES, 246);

            ThisRecordCounter = BitConverter.ToInt16(new byte[] { RAWBYTES[252],RAWBYTES[253] }, 0);
        }

        public DateTime Decode_Date(byte[] RAWBYTES,int start_index)
        {
            int year = RAWBYTES[start_index] + 2000;
            int month = RAWBYTES[ start_index + 1 ] & 0x0f;
            int date = RAWBYTES[ start_index + 2 ];
            int hour = RAWBYTES[ start_index + 3 ];
            int minute = RAWBYTES[ start_index + 4 ];
            int second = RAWBYTES[start_index + 5];
            return new DateTime(year, month, date, hour, minute, second);
        }
        
    }

   
}
