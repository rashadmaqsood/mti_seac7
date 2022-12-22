//#define SHENZHEN_CLOU_Heartbeat
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
    public abstract class HeartBeat
    {
        #region Data_Members
        /// <summary>
        /// HeartBeat/KeepAlive Packet Start Header TAG(OXDD)
        /// </summary>
        public static byte HeartBeatTag = 0x0A;
        /// <summary>
        /// HeartBeat/KeepAlive Fix Response Packet
        /// </summary>
        public static byte[] HeartBeatResponse = new byte[] { 0xAA };
        /// <summary>
        /// HeartBeat Request Packet Fixed Header Length 0x08
        /// </summary>
        public static byte HeartBeatMaxLength = 30;
        
        private byte[] mSerialNo = new byte[0x04];
        private ushort eventCounter;
        private bool isVerifited;
        private DateTime dateTimeStamp;
        
        #endregion

        #region Public Properties

        /// <summary>
        /// The Meter Serial Number (MSN) decoded from HeartBeat Packet
        /// </summary>
        public byte[] MeterSerialNo
        {
            get { return mSerialNo; }
            set { mSerialNo = value; }
        }
        /// <summary>
        /// The Meter Serial Number (MSN) String representation decoded from HeartBeat Packet
        /// </summary>
        public string MeterSerialStr
        {
            get
            {
                long number = 0;
                String str = null;
                if (MeterSerialNo != null && MeterSerialNo.Length == MeteSerialNoLength)
                {

                    for (int index = 0; index < MeterSerialNo.Length; index++)
                    {
                        if (index == 0)
                            number = MeterSerialNo[0];
                        else
                        {
                            number = number << 8;
                            number = number | MeterSerialNo[index];
                        }
                        str = number.ToString();
                    }
                }
                else
                {
                    str = ASCIIEncoding.ASCII.GetString(MeterSerialNo);
                }
                return str;
            }

        }
        /// <summary>
        /// The Meter Serial Number bytes length 
        /// </summary>
        public uint MeteSerialNoLength
        {
            get
            {
                if (MeterSerialNo == null)
                    return 0;
                else
                    return (uint)MeterSerialNo.Length;
            }
            set
            {
                if (MeterSerialNo != null && MeterSerialNo.Length == value)
                    return;
                else
                    MeterSerialNo = new byte[value];
            }
        }
        /// <summary>
        /// The dateTime stamp when current HeartBeat packet received
        /// </summary>
        public DateTime DateTimeStamp
        {
            get { return dateTimeStamp; }
            set { dateTimeStamp = value; }
        }

        /// <summary>
        /// Represent the Number of Events occurred in Metering Device since last HeartBeat Packet sent
        /// </summary>
        public ushort EventCounter
        {
            get { return eventCounter; }
            set { eventCounter = value; }
        }
        
        /// <summary>
        /// It will Indicate that the HeartBeat Packet received successfully Decoded
        /// </summary>
        public bool IsVerifited
        {
            get { return isVerifited; }
            set { isVerifited = value; }
        }
        /// <summary>
        /// Get the expected heartbeat packet length
        /// </summary>
        public int ExpectedPacketLength
        {
            get
            {
                return (int)MeteSerialNoLength + 2;
            }
        }

        #endregion

        #region Member_Methods

        /// <summary>
        /// This method decodes the HeartBeat Packet from Buffer, then store the MSN,Event_Counter
        /// details in their respective properties
        /// </summary>
        /// <param name="APDU">Encoded HeartBeat Packet Buffer</param>
        /// <param name="index">Start Index to Buffer</param>
        /// <param name="size">Number of Bytes in HeartBeat Packet</param>
        public abstract void DecodeHeartBeat(byte[] APDU, int index, int size);

        /// <summary>
        /// Encodes the HeartBeat Response Packet
        /// </summary>
        /// <returns>byte[] encoded HeartBeat Response Packet</returns>
        public static byte[] EncodeHeartBeat()
        {
            try
            {
                return HeartBeat.HeartBeatResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        /// <summary>
        /// String representation of HeartBeat
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("MSN:{0},Event Count{1},TimeStamp:{2}", MeterSerialStr, EventCounter, this.dateTimeStamp);
        }
    }
}

