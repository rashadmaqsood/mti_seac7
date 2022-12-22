using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;

namespace DLMS
{
    /// <summary>
    /// The WrapperLayer is DLMS_COSEM Transport Layer +COM Wrapper,the instance of
    /// this class apply +COMWrapper before transmit on TCP/UDP transport layer and also
    /// detach the +COMWrapper from IO Packet received from TCP/UDP transport layer.
    /// The +COMWrapper store DLMS_COSEM compliant meter device <see cref="SAP_Object"/> and
    /// Client COSEM transport layer <see cref="SAP_Object"/>.
    /// </summary>
    public class WrapperLayer
    {
        #region DataMembers
        
        /// <summary>
        /// Constant +COMWrapper header length 0x08
        /// </summary>
        public static readonly int WrapperHeaderLength = 0x08;
        private ushort Wrapper_Version = 0x0001;
        private ushort Meter_SAP;
        private ushort Current_Client_SAP;
        private ushort packetLength;
        private bool isVerified;

        public static readonly WrapperLayer Default = null;

        #endregion

        #region Properties

        /// <summary>
        /// Get/Set the Client COSEM transport layer SAP identifier <seealso cref="SAP_Object"/>
        /// </summary>
        public ushort CurrentClientSAP
        {
            get { return Current_Client_SAP; }
            set { Current_Client_SAP = value; }
        }

        /// <summary>
        /// Get/Set DLMS_COSEM compliant device SAP identifier <seealso cref="SAP_Object"/>
        /// </summary>
        public ushort MeterSAP
        {
            get { return Meter_SAP; }
            set { Meter_SAP = value; }
        }

        /// <summary>
        /// Fixed +COMWrapper header version number 0x01
        /// </summary>
        public ushort WrapperVersion
        {
            get { return Wrapper_Version; }
            set { Wrapper_Version = value; }
        }

        /// <summary>
        /// DLMS_COSEM Application Layer APDU(Application process Data Unit) total length
        /// </summary>
        public ushort PacketLength
        {
            get { return packetLength; }
            set { packetLength = value; }
        }

        /// <summary>
        /// Is received Packet +COMWrapper Verified
        /// </summary>
        public bool IsVerified
        {
            get { return isVerified; }
            set { isVerified = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="MeterSAP"><see cref="MeterSAP"/></param>
        /// <param name="ClientSAP"><see cref="CurrentClientSAP"/></param>
        public WrapperLayer(ushort MeterSAP, ushort ClientSAP)
        {
            this.MeterSAP = MeterSAP;
            this.CurrentClientSAP = ClientSAP;
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public WrapperLayer()
            : this(0x10, 0x01)
        {
        }

        /// <summary>
        ///Static Constructor
        /// </summary>
        static WrapperLayer()
        {
            Default = new WrapperLayer(DLMS_Common.ManagementDevice.SAP_Address, DLMS_Common.Public.SAP_Address);
        }

        #endregion

        #region MemberMethods

        /// <summary>
        /// This method append +COMWrapper at the start of APDU Buffer parameter
        /// </summary>
        /// <param name="APDU">Data Buffer</param>
        public void Apply_Wrapper(ref byte[] APDU)
        {
            try
            {
                // 2 bytes for length
                // simple length is to be used not the encoded length
                UInt16 APDU_Length = (UInt16)APDU.Length;
                APDU = DLMS_Common.Append_to_Start(APDU, APDU_Length);
                //APDU = Common.Append_Length_to_Start(APDU);

                // 2 bytes each for Destination SAP and Source SAP
                APDU = DLMS_Common.Append_to_Start(APDU, MeterSAP);
                APDU = DLMS_Common.Append_to_Start(APDU, CurrentClientSAP);
                APDU = DLMS_Common.Append_to_Start(APDU, WrapperVersion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method encode +COMWrapper from current instance
        /// </summary>
        
        public byte[] EncodeWrapper()
        {
            try
            {
                byte[] WrapperBytes = new byte[8];
                WrapperBytes[0] = (byte)((WrapperVersion) >> 8);
                WrapperBytes[1] = (byte)((WrapperVersion) & 0x00ff);
                WrapperBytes[2] = (byte)((CurrentClientSAP) >> 8);
                WrapperBytes[3] = (byte)((CurrentClientSAP) & 0x00ff);
                WrapperBytes[4] = (byte)((MeterSAP) >> 8);
                WrapperBytes[5] = (byte)((MeterSAP) & 0x00ff);
                WrapperBytes[6] = (byte)((PacketLength) >> 8);
                WrapperBytes[7] = (byte)((PacketLength) & 0x00ff);

                return WrapperBytes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method decode +COMWrapper at the start of APDU Buffer parameter and update <see cref="IsVerified"/>
        /// </summary>
        /// <param name="APDU">Data Buffer</param>
        /// <param name="index">Start index for Data Buffer</param>
        /// <param name="length">Total byte length</param>
        public void DecodeWrapper(byte[] APDU, int index, int length)
        {
            try
            {
                PacketLength = 0;
                IsVerified = false;
                if (APDU != null &&
                    index + length <= APDU.Length &&
                    index >= 0 && length > 0)
                {
                    byte a1 = (byte)((WrapperVersion) >> 8);
                    byte a2 = (byte)((WrapperVersion) & 0x00ff);
                    byte a3 = (byte)((MeterSAP) >> 8);
                    byte a4 = (byte)((MeterSAP) & 0x00ff);
                    byte a5 = (byte)((CurrentClientSAP) >> 8);
                    byte a6 = (byte)((CurrentClientSAP) & 0x00ff);

                    if (APDU[0 + index] == a1 &&
                        APDU[1 + index] == a2 &&
                        APDU[2 + index] == a3 &&
                        APDU[3 + index] == a4 &&
                        APDU[4 + index] == a5 &&
                        APDU[5 + index] == a6)
                    {
                        UInt16 length_APDU = (UInt16)(((UInt16)(APDU[6]) << 8) | APDU[7]);
                        PacketLength = length_APDU;
                        IsVerified = true;
                    }
                    else
                        isVerified = false;
                }
                else
                    throw new ArgumentNullException("Invalid argument passed");
            }
            catch (Exception)
            {
                isVerified = false;
            }
        }

        /// <summary>
        /// This method decode +COMWrapper at the start of APDU Buffer parameter and update <see cref="IsVerified"/>
        /// </summary>
        /// <param name="APDU">Data Buffer</param>
        /// <param name="index">Start index for Data Buffer</param>
        /// <param name="length">Total byte length</param>
        public bool TryDecodeWrapper(byte[] APDU, int index, int length, out int PacketLengthArg)
        {
            bool isVerifiedLocal = false;
            PacketLengthArg = 0;

            try
            {
                isVerifiedLocal = false;

                if (APDU != null &&
                    index + length <= APDU.Length &&
                    index >= 0 && length > 0)
                {
                    byte a1 = (byte)((WrapperVersion) >> 8);
                    byte a2 = (byte)((WrapperVersion) & 0x00ff);
                    byte a3 = (byte)((MeterSAP) >> 8);
                    byte a4 = (byte)((MeterSAP) & 0x00ff);
                    byte a5 = (byte)((CurrentClientSAP) >> 8);
                    byte a6 = (byte)((CurrentClientSAP) & 0x00ff);

                    if (APDU[0 + index] == a1 &&
                        APDU[1 + index] == a2 &&
                        APDU[2 + index] == a3 &&
                        APDU[3 + index] == a4 &&
                        APDU[4 + index] == a5 &&
                        APDU[5 + index] == a6)
                    {
                        ushort length_APDU = (ushort)(((ushort)(APDU[6]) << 8) | APDU[7]);
                        PacketLengthArg = length_APDU;
                        isVerifiedLocal = true;
                    }
                    else
                        isVerifiedLocal = false;
                }
                else
                    throw new ArgumentNullException("Invalid argument passed");
            }
            catch (Exception)
            {
                isVerifiedLocal = false;
            }

            return isVerifiedLocal;
        }


        /// <summary>
        /// This method decode +COMWrapper at the start of APDU Buffer parameter and remove wrapper bytes
        /// </summary>
        /// <param name="APDU"></param>
        /// <returns>bool:Is +COMWrapper Verified or not</returns>
        public bool Remove_Wrapper(ref byte[] APDU)
        {
            bool Successful = false;

            byte a1 = (byte)((WrapperVersion) >> 8);
            byte a2 = (byte)((WrapperVersion) & 0x00ff);
            byte a3 = (byte)((MeterSAP) >> 8);
            byte a4 = (byte)((MeterSAP) & 0x00ff);
            byte a5 = (byte)((CurrentClientSAP) >> 8);
            byte a6 = (byte)((CurrentClientSAP) & 0x00ff);

            /*if (APDU[0] == (DLMS_Application_Process.Wrapper_Version >> 8) &&
                APDU[1] == (DLMS_Application_Process.Wrapper_Version & 0x00ff) &&
                APDU[2] == (DLMS_Application_Process.Meter_SAP >> 8) &&
                APDU[3] == (DLMS_Application_Process.Meter_SAP & 0x00ff) &&
                APDU[4] == (DLMS_Application_Process.Client_SAP >> 8) &&
                APDU[5] == (DLMS_Application_Process.Client_SAP & 0x00ff)
                )*/

            if (APDU[0] == a1 &&
                APDU[1] == a2 &&
                APDU[2] == a3 &&
                APDU[3] == a4 &&
                APDU[4] == a5 &&
                APDU[5] == a6
                )
            {
                UInt16 length_APDU = (UInt16)(((UInt16)(APDU[6]) << 8) | APDU[7]);

                if (length_APDU == APDU.Length - 8)
                {
                    Successful = true;
                    byte[] Wrapper_Less = new byte[APDU.Length - 8];
                    DLMS_Common.Byte_Array_Copier(APDU, ref Wrapper_Less, 8);
                    DLMS_Common.Byte_Array_Copier(Wrapper_Less, ref APDU, 0);
                }
            }
            return Successful;
        }

        #endregion
    }
}