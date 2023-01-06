using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLMS
{
    /// <summary>
    /// The HeartBeat instance holds/decode information about HeartBeat(KEEP_ALIVE Packet)
    /// sent by the Remote Metering Device, the HeartBeat will sent on IPv4 network to avoid 
    /// Network disconnection due to Inactivity Timer Elapsed on Network Transport Layer.
    /// </summary>
    public class HeartBeat_YTL:HeartBeat
    {
        public HeartBeat_YTL()
        {
            HeartBeatTag = 0x0D;
            HeartBeatResponse = new byte[] { 0xDA };
            HeartBeatMaxLength = 6;
        }

        #region Member_Methods
        
        /// <summary>
        /// This method decodes the HeartBeat Packet from Buffer, then store the MSN,Event_Counter
        /// details in their respective properties
        /// </summary>
        /// <param name="APDU">Encoded HeartBeat Packet Buffer</param>
        /// <param name="index">Start Index to Buffer</param>
        /// <param name="size">Number of Bytes in HeartBeat Packet</param>

        public override void DecodeHeartBeat(byte[] APDU, int index, int size)
        {
            try
            {
                if (APDU != null && ((index + size) <= APDU.Length && index >= 0) && size > 0)
                {
                    if (APDU[index++] == HeartBeatTag)
                    {
                        uint mSerialNoLength = (uint)APDU[index++];
                        MeteSerialNoLength = mSerialNoLength;
                        if ((size - index) >= MeteSerialNoLength && MeteSerialNoLength > 0)
                        {
                            for (int i = 0; (i < mSerialNoLength && index < APDU.Length); index++, i++)
                                MeterSerialNo[i] = APDU[index];
                            try
                            {
                                EventCounter = (UInt16)(((UInt16)(APDU[index]) << 8) | APDU[index + 1]);
                            }
                            catch (Exception)
                            { }
                            IsVerifited = true;
                            DateTimeStamp = DateTime.Now;
                        }
                        else
                            IsVerifited = false;
                    }
                    else
                    {
                        DateTimeStamp = new DateTime();
                        IsVerifited = false;
                    }

                }
                else
                    throw new ArgumentNullException("Invalid argument passed");
            }
            catch (Exception ex)
            {
                IsVerifited = false;
            }
        }

 
        #endregion

    }
}

