using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SharedCode.Comm.DataContainer
{
    public class EventIDtoCode
    {
        Dictionary<int, int> EventDetail = new Dictionary<int, int>();
        public EventIDtoCode()
        {
            EventDetail.Add(1, 120);
            EventDetail.Add(2, 124);
            EventDetail.Add(3, 118);
            EventDetail.Add(4, 113);
            EventDetail.Add(5, 115);
            EventDetail.Add(6, 114);
            EventDetail.Add(7, 119);
            EventDetail.Add(8, 123);
            EventDetail.Add(9, 201);
            EventDetail.Add(10, 117);
            EventDetail.Add(11, 128);
            EventDetail.Add(12, 121);
            EventDetail.Add(13, 122);
            EventDetail.Add(14, 109);
            EventDetail.Add(15, 111);
            EventDetail.Add(16, 112);
            EventDetail.Add(17, 103);
            EventDetail.Add(18, 203);
            EventDetail.Add(19, 204);
            EventDetail.Add(20, 205);
            EventDetail.Add(21, 116);
            EventDetail.Add(22, 110);
            EventDetail.Add(23, 206);
            EventDetail.Add(24, 101);
            EventDetail.Add(25, 102);
            EventDetail.Add(26, 207);
            EventDetail.Add(27, 104);
            EventDetail.Add(28, 105);
            EventDetail.Add(29, 106);
            EventDetail.Add(30, 208);
            EventDetail.Add(31, 107);
            EventDetail.Add(32, 108);
            EventDetail.Add(33, 125);
            EventDetail.Add(34, 126);
            EventDetail.Add(35, 127);
            EventDetail.Add(36, 209);
            EventDetail.Add(37, 210);
            EventDetail.Add(38, 211);
            EventDetail.Add(39, 212);
            EventDetail.Add(40, 213);
            EventDetail.Add(41, 214);
            EventDetail.Add(42, 215);
            EventDetail.Add(43, 216);
            EventDetail.Add(44, 217);
            EventDetail.Add(45, 218);
            EventDetail.Add(46, 219);
            EventDetail.Add(47, 220);
            EventDetail.Add(48, 221);

            EventDetail.Add(49, 225);
            EventDetail.Add(50, 226);
            EventDetail.Add(51, 227);
            EventDetail.Add(52, 228);
            EventDetail.Add(53, 229);
            EventDetail.Add(54, 230);
            EventDetail.Add(55, 231);
            EventDetail.Add(56, 232);
            EventDetail.Add(57, 233);
            EventDetail.Add(58, 234);
            EventDetail.Add(59, 235);
            EventDetail.Add(60, 236);

        }
        public int getEventCode(int EventID)
        {
            // See whether Dictionary contains this string.
            if (EventDetail.ContainsKey(EventID))
            {
                int value = EventDetail[EventID];
                return value;
            }

            // See whether Dictionary contains this string.
            if (!EventDetail.ContainsKey(EventID))
            {
                return -1;
            }
            return -1;
        }

        public int getEventID(int eventCode)
        {
            try
            {
                if (EventDetail.ContainsValue(eventCode))
                {
                    KeyValuePair<int, int> c = EventDetail.First(x => x.Value == eventCode);
                    return c.Key;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
