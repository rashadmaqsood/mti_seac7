
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

namespace _HDLC
{
    public struct HDLCFrame
    {
        #region DataMembers

        #region Constants

        public static readonly byte FrameTag = 0x7E;
        public static readonly byte DLMSProtocolId = 0xA;
        public static readonly uint IAPDU_Req_TAG = 0x00E6E6;
        public static readonly uint IAPDU_Res_TAG = 0x00E7E6;
        public static readonly ushort SNRM_APDU_Req_TAG = 0x8081;
        public static readonly byte APDU_Buf_TTAG = 0x5;
        public static readonly byte APDU_Buf_RTAG = 0x6;
        public static readonly byte WinSize_TTAG = 0x7;
        public static readonly byte WinSize_RTAG = 0x8;

        public static readonly byte DEFAULT_HDLC_ADDRESSLENGTH = 0x4;

        #endregion

        private ushort _frameFormat;
        private bool isInfoFirstFrame;
        private AddressLength _DAAddressLength;
        private byte[] _destinationAddr;
        private byte[] _sourceAddr;
        private byte _controlField;
        private ushort _hcs;
        private byte[] _APDU;
        private ushort _fcs;
        private byte WinSizeT;
        private byte WinSizeR;
        private ushort APDU_Buf_R;
        private ushort APDU_Buf_T;
        private string errorMessage;
        private bool isSkipAPDU;

        #endregion

        #region Constructors

        public HDLCFrame(bool t)
        {
            WinSizeT = 1;
            WinSizeR = 1;
            APDU_Buf_R = 0x128;
            APDU_Buf_T = 0x128;
            _frameFormat = 0xA000;
            _controlField = (byte)FrameType.None;
            _destinationAddr = null;
            _DAAddressLength = AddressLength.Four_Byte;
            _sourceAddr = null;
            _hcs = 0;
            _APDU = null;
            _fcs = 0;
            isInfoFirstFrame = true;
            errorMessage = null;
            isSkipAPDU = false;
        }

        public HDLCFrame(FrameType frameType)
            : this(true)
        {
            _FrameType = frameType;
            errorMessage = null;
        }
        #endregion

        #region Properties

        // Frame Format Properties
        public ushort FrameFormat
        {
            get { return _frameFormat; }
            set
            {
                if (veriftyFrameFormat(value))
                    _frameFormat = value;
                else
                    throw new Exception("Invalid Frame Format");
            }
        }

        public ushort FrameLength
        {
            get
            {
                return (ushort)(_frameFormat & 0x07FF);
            }
            set
            {
                if (value < 2048) // 11 bit frameSize
                {
                    _frameFormat = (ushort)(_frameFormat & 0xF800);
                    _frameFormat = (ushort)(_frameFormat | (value & 0x07FF));
                }
                else
                    throw new Exception("Invalid Frame Length");
            }
        }

        public bool IsSegmented
        {
            get
            {
                if ((_frameFormat & 0x0800) > 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)   // Segment On
                    _frameFormat = (ushort)(_frameFormat | 0x0800);
                else
                    _frameFormat = (ushort)(_frameFormat & 0xF7FF);
            }
        }

        public AddressLength DestinationAddressLength
        {
            get
            {
                return _DAAddressLength;
            }
            set
            {
                _DAAddressLength = value;
            }
        }

        // Destination Address Properties
        public byte[] DestinationHDLCAddress
        {
            get { return _destinationAddr; }
            set
            {
                // Verify_Data
                if (verifyAddress(value))
                    _destinationAddr = value;
                else
                    throw new Exception("Invalid Address");
            }
        }

        /// <summary>
        /// GET_SET Plain UpperHDLC Address
        /// </summary>
        public ushort UpperHDLCDestAddress
        {
            get
            {
                return GetUpperHDLCAddress(ref _destinationAddr);
            }
            set
            {
                SetUpperHDLCAddress(ref _destinationAddr, value, DestinationAddressLength);
            }
        }

        public ushort LowerHDLCDestAddress
        {
            get
            {
                return GetLowerHDLCAddress(ref _destinationAddr);
            }
            set
            {
                SetLowerHDLCAddress(ref _destinationAddr, value, DestinationAddressLength);
            }
        }

        public byte ClientHDLCDestAddress
        {
            get { return GetClientAddress(ref _destinationAddr); }
            set { SetClientAddress(ref _destinationAddr, value); }
        }

        /// Source HDLC Address
        public byte[] SourceHDLCAddress
        {
            get { return _sourceAddr; }
            set
            {
                if (verifyAddress(value))
                    _sourceAddr = value;
                else
                    throw new Exception("Invalid Source HDLC Address");
            }
        }

        public ushort UpperHDLCSourceAddress
        {
            get
            {
                return GetUpperHDLCAddress(ref _sourceAddr);
            }
            set
            {
                SetUpperHDLCAddress(ref _sourceAddr, value, AddressLength.One_Byte);
            }
        }

        public ushort LowerHDLCSourceAddress
        {
            get
            {
                return GetLowerHDLCAddress(ref _sourceAddr);
            }
            set
            {
                SetLowerHDLCAddress(ref _sourceAddr, value, AddressLength.One_Byte);
            }
        }

        public byte ClientHDLCSourceAddress
        {
            get { return GetClientAddress(ref _sourceAddr); }
            set { SetClientAddress(ref _sourceAddr, value); }
        }
        /// <summary>
        /// One Byte HDLC Frame Control Field,defines the FrameType,ReceiveWindow,Transmit Window and 
        /// Poll/Final Bits
        /// </summary>
        public byte ControlField
        {
            get { return _controlField; }
            set
            {
                FrameType t = FrameType.DM;
                if (VerifyFrameType(value, out t))
                    _controlField = value;
                else
                    throw new HDLCInValidFrameException("Invalid Frame,Invalid Control Field");
            }
        }
        public FrameType _FrameType
        {
            get
            {
                FrameType t = FrameType.DM;
                if (VerifyFrameType(ControlField, out t))
                    return t;
                else
                    throw new HDLCInValidFrameException("Invalid HDLC Frame");
            }
            set
            {
                bool p_f = true;                ///Preserve P/F Status
                if ((_controlField & 0xEF) > 0)
                    p_f = true;
                else
                    p_f = false;
                switch (value)
                {
                    #region Cases

                    case FrameType.I:
                        _controlField = (byte)(_controlField & 0xFE);
                        _controlField = (byte)(_controlField | (byte)FrameType.I);
                        if (!p_f)   //Reset p_f 
                            _controlField = (byte)(_controlField & 0xEF);
                        break;
                    case FrameType.RR:
                        _controlField = (byte)(_controlField & 0xF1);
                        _controlField = (byte)(_controlField | (byte)FrameType.RR);
                        if (!p_f)   //Reset p_f 
                            _controlField = (byte)(_controlField & 0xEF);
                        break;
                    case FrameType.RNR:
                        _controlField = (byte)(_controlField & 0xF5);
                        _controlField = (byte)(_controlField | (byte)FrameType.RNR);
                        if (!p_f)   //Reset p_f 
                            _controlField = (byte)(_controlField & 0xEF);
                        break;
                    case FrameType.SNRM:
                        _controlField = (byte)(_controlField & 0x93);
                        _controlField = (byte)(_controlField | (byte)FrameType.SNRM);
                        if (!p_f)   //Reset p_f 
                            _controlField = (byte)(_controlField & 0xEF);
                        break;
                    case FrameType.DISC:
                        _controlField = (byte)(_controlField & 0x53);
                        _controlField = (byte)(_controlField | (byte)FrameType.DISC);
                        if (!p_f)   //Reset p_f 
                            _controlField = (byte)(_controlField & 0xEF);
                        break;
                    case FrameType.UA:
                        _controlField = (byte)(_controlField & 0x73);
                        _controlField = (byte)(_controlField | (byte)FrameType.UA);
                        if (!p_f)   //Reset p_f 
                            _controlField = (byte)(_controlField & 0xEF);
                        break;
                    case FrameType.DM:
                        _controlField = (byte)(_controlField & 0x1F);
                        _controlField = (byte)(_controlField | (byte)FrameType.DM);
                        if (!p_f)   //Reset p_f 
                            _controlField = (byte)(_controlField & 0xEF);
                        break;
                    case FrameType.FRMR:
                        _controlField = (byte)(_controlField & 0x97);
                        _controlField = (byte)(_controlField | (byte)FrameType.FRMR);
                        if (!p_f)   //Reset p_f 
                            _controlField = (byte)(_controlField & 0xEF);
                        break;
                    case FrameType.UI:
                        _controlField = (byte)(_controlField & 0x13);
                        _controlField = (byte)(_controlField | (byte)FrameType.UI);
                        if (!p_f)   //Reset p_f 
                            _controlField = (byte)(_controlField & 0xEF);
                        break;

                    #endregion
                }

            }
        }

        public bool P_F
        {
            get
            {
                if ((_controlField & 0x10) > 0)
                    return true;
                else
                    return false;

            }
            set
            {
                if (value) ///Set P_F
                    _controlField = (byte)(_controlField | 0x10);
                else       ///Reset P_F
                    _controlField = (byte)(_controlField & 0xEF);
            }
        }

        public byte ReceiveWindowNo
        {
            get
            {
                return (byte)((ControlField & 0xE0) >> 5);
            }
            set
            {
                if (value >= 0 && value <= 7)
                {
                    _controlField = (byte)(_controlField & 0x1F);
                    _controlField = (byte)(_controlField | (value << 5));
                }
                else
                    throw new Exception("Invalid Window No");
            }
        }

        public byte TransmitWindowNo
        {
            get
            {
                return (byte)((ControlField & 0x0E) >> 1);
            }
            set
            {
                if (value >= 0 && value <= 7)
                {
                    _controlField = (byte)(_controlField & 0xF1);
                    _controlField = (byte)(_controlField | (value << 1));
                }
                else
                    throw new Exception("Invalid Window No");
            }
        }

        public ushort HCS
        {
            get { return _hcs; }
            set { _hcs = value; }
        }

        public bool IsInfoFirstFrame
        {
            get { return isInfoFirstFrame; }
            set { isInfoFirstFrame = value; }
        }

        public byte[] APDU
        {
            get { return _APDU; }
            set { _APDU = value; }
        }

        public ushort FCS
        {
            get { return _fcs; }
        }

        public byte WinSizeTransmit
        {
            get { return WinSizeT; }
            set { WinSizeT = value; }
        }

        public byte WinSizeReceive
        {
            get { return WinSizeR; }
            set { WinSizeR = value; }
        }

        public ushort APDU_Buf_Transmit
        {
            get { return APDU_Buf_T; }
            set { APDU_Buf_T = value; }
        }

        public ushort APDU_Buf_Receive
        {
            get { return APDU_Buf_R; }
            set { APDU_Buf_R = value; }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
        }

        public bool IsSkipAPDU
        {
            get { return isSkipAPDU; }
            set { isSkipAPDU = value; }
        }

        #endregion

        #region Member Methods

        private bool veriftyFrameFormat(ushort frameFormat)
        {
            byte tVal = (byte)(frameFormat >> 12);
            if (tVal == DLMSProtocolId)
                return true;
            else
                return false;
        }

        private bool verifyAddress(byte[] addr)
        {
            if (addr == null || !(addr.Length >= 1 && addr.Length <= 4) || addr.Length == 3)
                return false;
            else
            {
                for (int index = 0; index < addr.Length; index++)
                {
                    if (index == 0)  // Check LSB,1 Byte Set
                    {
                        if ((addr[index] & 0x01) > 0)
                            continue;
                        else
                            return false;
                    }
                    else           // Check LSB Reset
                    {
                        if ((addr[index] & 0x01) == 0)
                            continue;
                        else
                            return false;
                    }
                }

                return true;
            }
        }

        private bool VerifyFrameType(byte controField, out FrameType fr)
        {
            controField = (byte)(controField | 0x10);       ///Set Poll/Final
            // Check Value Against Masks
            if ((byte)(controField & 0x11) == (byte)FrameType.I)
            {
                fr = FrameType.I;
                return true;
            }
            else if ((byte)(controField & 0x1F) == (byte)FrameType.RR)
            {
                fr = FrameType.RR;
                return true;
            }
            else if ((byte)(controField & 0x1F) == (byte)FrameType.RNR)
            {
                fr = FrameType.RNR;
                return true;
            }
            else if ((byte)(controField & 0xFF) == (byte)FrameType.SNRM)
            {
                fr = FrameType.SNRM;
                return true;
            }
            else if ((byte)(controField & 0xFF) == (byte)FrameType.UA)
            {
                fr = FrameType.UA;
                return true;
            }
            else if ((byte)(controField & 0xFF) == (byte)FrameType.DISC)
            {
                fr = FrameType.DISC;
                return true;
            }
            else if ((byte)(controField & 0xFF) == (byte)FrameType.DM)
            {
                fr = FrameType.DM;
                return true;
            }
            else if ((byte)(controField & 0xFF) == (byte)FrameType.FRMR)
            {
                fr = FrameType.FRMR;
                return true;
            }
            else if ((byte)(controField & 0xFF) == (byte)FrameType.UI)
            {
                fr = FrameType.UI;
                return true;
            }
            else
            {
                fr = FrameType.DM;
                return false;
            }
        }

        public void SetUpperHDLCAddress(ref byte[] Addr, ushort value, AddressLength addressLength)
        {
            if (Addr == null)
            {
                Addr = new byte[(byte)addressLength];
            }
            else
            {
                #region // Change Address Size

                if (Addr.Length != (int)addressLength)
                {
                    byte[] t = null;
                    t = new byte[(byte)addressLength];
                    // Address Expenssion Case
                    if (Addr.Length < (int)addressLength)
                    {
                        if (Addr.Length == 2)
                        {
                            t[0] = Addr[0];
                            t[1] = 0;
                        }
                    }
                    // Shrink Address Case
                    else
                    {
                        if ((int)addressLength == 2)
                        {
                            t[0] = Addr[0];
                        }
                    }
                    Addr = t;
                }

                #endregion // // Change Address Size
            }

            #region SetValue

            if ((int)addressLength == 1)
            {
                Addr[0] = (byte)(value << 1);
            }
            else if ((int)addressLength == 2)
            {
                Addr[1] = (byte)(value << 1);
            }
            else
            {
                Addr[2] = (byte)(value << 1);
                //Addr[3] = (byte)((value >> 7) & 0xF7);
                Addr[3] = (byte)((value >> 7) << 1);
            }

            #endregion
            #region // Set_LSB

            // Reset LSB 
            for (int index = Addr.Length - 1; index >= 1; index--)
            {
                Addr[index] &= 0xFE;
            }
            // Set LSB
            Addr[0] |= 0x01;

            #endregion // MyRegion
        }

        public ushort GetUpperHDLCAddress(ref byte[] Addr)
        {
            ushort retVal = ushort.MaxValue;
            if (Addr != null)
            {
                if (Addr.Length == 1 || Addr.Length == 2)
                {
                    retVal = Addr[Addr.Length - 1];
                    retVal = (ushort)(retVal >> 1);
                }
                else
                {
                    //retVal = (byte)(((Addr[3] << 6) & 0xFE) | Addr[2] >> 1);
                    retVal = (ushort)(((Addr[3] >> 1) << 7) | Addr[2] >> 1);

                }

            }
            return retVal;
        }

        public void SetLowerHDLCAddress(ref byte[] Addr, ushort value, AddressLength addressLength)
        {
            if (Addr == null)
            {
                Addr = new byte[(byte)addressLength];
            }
            else
            {
                #region Change Address Size

                if (Addr.Length != (int)addressLength) // Change Address Size
                {
                    byte[] t = null;
                    t = new byte[(byte)addressLength];
                    // Address Expenssion Case
                    if (Addr.Length < (int)addressLength)
                    {
                        if (Addr.Length == 1 && (int)addressLength == 2)
                        {
                            t[1] = Addr[0];
                        }
                        else if (Addr.Length == 1 && (int)addressLength == 4)
                        {
                            t[2] = Addr[0];
                            t[3] = 0;
                        }
                        else if (Addr.Length == 2 && (int)addressLength == 4)
                        {
                            t[2] = Addr[0];
                            t[3] = Addr[1];
                        }
                    }
                    // Shrink Address Case
                    else
                    {
                        if (Addr.Length == 4 && (int)addressLength == 1)
                        {
                            t[0] = Addr[2];
                        }
                        else if (Addr.Length == 4 && (int)addressLength == 2)
                        {
                            t[1] = Addr[2];
                        }
                        else if (Addr.Length == 2 && (int)addressLength == 1)
                        {
                            t[0] = Addr[1];
                        }
                    }
                    Addr = t;
                }

                #endregion // Change Address Size
            }

            #region SET Value

            if ((int)addressLength == 2)
            {
                Addr[0] = (byte)(value << 1);
            }
            else if ((int)addressLength == 4)
            {
                Addr[0] = (byte)(value << 1);
//                Addr[1] = (byte)((value >> 7) & 0xF7);
                Addr[1] = (byte)((value >> 7) << 1);
            }

            #endregion
            #region // Set_LSB

            // Reset LSB 
            for (int index = Addr.Length - 1; index >= 1; index--)
            {
                Addr[index] &= 0xFE;
            }
            // Set LSB
            Addr[0] |= 0x01;

            #endregion // MyRegion

            //  Previous Code
            // if (Addr == null)
            // {
            //     Addr = new byte[4];
            // }
            // else
            // {
            //     if (Addr.Length == 2 || Addr.Length == 1)///Increase Size 4
            //     {
            //         byte[] t = Addr;
            //         Addr = new byte[4];
            //         if (t.Length == 2) ///Copy Previous Address
            //             Addr[2] = t[1];
            //     }
            // }
            // if (value <= 127)    ///Single Byte
            // {
            //     Addr[0] = (byte)((value << 0x01) | 0x01);
            //     Addr[1] = 0;

            // }
            // else
            // {
            //     Addr[0] = (byte)((value << 0x01) | 0x01);
            //     Addr[1] = (byte)((value >> 7) & 0xF7);
            // }
        }

        public ushort GetLowerHDLCAddress(ref byte[] Addr)
        {
            ushort retVal = ushort.MaxValue;
            if (Addr != null)
            {
                if (Addr.Length == 2)
                {
                    retVal = Addr[0];
                    retVal = (ushort)(retVal >> 1);
                }
                else if (Addr.Length == 4)
                {
//                    retVal = (byte)(((Addr[1] << 6) & 0xFE) | Addr[0] >> 1);
                    retVal = (ushort)(((Addr[1] >>1) << 7) | Addr[0] >> 1);

                }
            }
            return retVal;
        }

        public byte GetClientAddress(ref byte[] Addr)
        {
            byte retVal = byte.MaxValue;
            if (Addr != null)
            {
                retVal = (byte)(Addr[0] >> 1);
            }
            return retVal;
        }

        public void SetClientAddress(ref byte[] Addr, byte val)
        {
            if (Addr == null || Addr.Length > 1)
                Addr = new byte[1];
            Addr[0] = (byte)((val << 0x01) | 0x01);
        }

        public byte[] ToByteArray()
        {
            try
            {
                List<byte> Frame = null;
                FrameType type = _FrameType;                    // Validating  HDLC Frame 
                if (!veriftyFrameFormat(FrameFormat))
                    FrameFormat = 0xA000;                       // Cosem ID & No Segmentation
                if (!(verifyAddress(DestinationHDLCAddress) && verifyAddress(SourceHDLCAddress)))
                    throw new HDLCInValidFrameException("Invalid HDLC Address");
                byte hStIndex = 1;
                byte hcsIndex = 0;
                byte fcsIndex = 0;
                byte APDUIndex = 0;
                ushort fLength;
                Frame = new List<byte>();
                Frame.Add(HDLCFrame.FrameTag);
                Frame.Add((byte)(FrameFormat >> 8));                                     // Encode FrameFormat With Blank
                Frame.Add((byte)0);

                for (int index = DestinationHDLCAddress.Length - 1; index >= 0; index--) // Adding Destination Address
                    Frame.Add(DestinationHDLCAddress[index]);
                for (int index = SourceHDLCAddress.Length - 1; index >= 0; index--)      // Adding Source Address
                    Frame.Add(SourceHDLCAddress[index]);

                Frame.Add(ControlField);                                                 // Adding Control Field
                hcsIndex = (byte)Frame.Count;
                Frame.Add(0x00);                                                         // Adding Blank HCS Fields
                Frame.Add(0x00);
                bool isAPDUAttach = false;
                APDUIndex = (byte)Frame.Count;

                // Skip Frame APDU Encoding
                if (!IsSkipAPDU)
                    switch (type)
                    {
                        case FrameType.I:                                                // Encoding Frame APDU I Field
                        case FrameType.UI:
                            if (APDU != null)
                            {
                                if (IsInfoFirstFrame)
                                {
                                    Frame.Add((byte)HDLCFrame.IAPDU_Req_TAG);
                                    Frame.Add((byte)(HDLCFrame.IAPDU_Req_TAG >> 8));
                                    Frame.Add((byte)(HDLCFrame.IAPDU_Req_TAG >> 16));
                                }
                                Frame.AddRange(APDU);
                                isAPDUAttach = true;
                            }
                            break;
                        case FrameType.SNRM:                                                // Encoding Frame SNRM/UA Field
                        case FrameType.UA:

                            Frame.Add((byte)HDLCFrame.SNRM_APDU_Req_TAG);
                            Frame.Add((byte)(HDLCFrame.SNRM_APDU_Req_TAG >> 8));
                            Frame.Add(0x12);                                                // Adding APDU Length
                            Frame.Add(HDLCFrame.APDU_Buf_TTAG);                             // Suggested Information Buf Transmit Size                
                            if (APDU_Buf_Transmit > 0xFF)
                            {
                                Frame.Add(0x2);                                             // Max Info Transmit Buf Size        
                                Frame.Add((byte)(APDU_Buf_Transmit >> 8));
                                Frame.Add((byte)(APDU_Buf_Transmit));
                            }
                            else
                            {
                                Frame.Add(0x1);                                             // Max Info Transmit Buf Size        
                                Frame.Add((byte)(APDU_Buf_Transmit));
                            }
                            Frame.Add(HDLCFrame.APDU_Buf_RTAG);                             // Suggested Information Buf Receive Size                
                            if (APDU_Buf_Receive > 0xFF)
                            {
                                Frame.Add(0x2);                                             // Max Info Receive Buf Size         
                                Frame.Add((byte)(APDU_Buf_Receive >> 8));
                                Frame.Add((byte)(APDU_Buf_Receive));
                            }
                            else
                            {
                                Frame.Add(0x1);                                             // Max Info Receive Buf Size        
                                Frame.Add((byte)(APDU_Buf_Transmit));
                            }
                            Frame.Add(HDLCFrame.WinSize_TTAG);                              // Max Window Transmit Size
                            Frame.Add(0x04);
                            Frame.Add(0x00);
                            Frame.Add(0x00);
                            Frame.Add(0x00);
                            Frame.Add(this.WinSizeTransmit);

                            Frame.Add(HDLCFrame.WinSize_RTAG);                              // Max Window Transmit Size
                            Frame.Add(0x04);
                            Frame.Add(0x00);
                            Frame.Add(0x00);
                            Frame.Add(0x00);
                            Frame.Add(this.WinSizeReceive);
                            isAPDUAttach = true;
                            break;
                    }

                fcsIndex = (byte)Frame.Count;                                                 // FcsIndex
                if (isAPDUAttach)
                {
                    Frame.Add(0x00);                                                        // Dumy FCS Bytes
                    Frame.Add(0x00);
                }
                Frame.Add(HDLCFrame.FrameTag);                                          // Frame End Tag
                fLength = (ushort)(Frame.Count - 2);                                    // Total Bytes In Frame - 2(Frame Tags)
                byte[] _frame = Frame.ToArray();
                // Update Length In Frame Format
                FrameLength = fLength;
                _frame[hStIndex] = (byte)(FrameFormat >> 8);
                _frame[hStIndex + 1] = (byte)(FrameFormat);
                // Compute HCS
                ushort _cmpHCS = 0;
                _cmpHCS = CheckSum.Calc_CRC16(_frame, hStIndex, hcsIndex - hStIndex);
                // Update HCS In Frame
                _frame[hcsIndex] = (byte)_cmpHCS;
                _frame[hcsIndex + 1] = (byte)(_cmpHCS >> 8);
                if (isAPDUAttach)
                {
                    ushort _cmpFCS = 0;
                    _cmpFCS = CheckSum.Calc_CRC16(_frame, hStIndex, fcsIndex - hStIndex);
                    // Update FCS In Frame
                    _frame[fcsIndex] = (byte)_cmpFCS;
                    _frame[fcsIndex + 1] = (byte)(_cmpFCS >> 8);
                }
                return _frame;
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(HDLCInValidFrameException))
                    throw ex;
                else
                    throw new HDLCInValidFrameException("Invalid Frame Format", ex);
            }
        }

        public bool TryParseAPDU(byte[] APDU, int Index, int Length)
        {
            try
            {
                using (BinaryReader data = new BinaryReader(new MemoryStream(APDU, Index, Length)))
                {
                    byte curByte = data.ReadByte();
                    byte[] tmpBuf = null;
                    byte APDUlength = 0;
                    if (curByte == 0xE6)           ///Expected APDU I Frame
                    {

                        uint tmp = curByte;
                        curByte = data.ReadByte();
                        tmp = (uint)(tmp | (curByte << 8));
                        curByte = data.ReadByte();
                        tmp = (uint)(tmp | (curByte << 16));
                        if (tmp == IAPDU_Res_TAG || tmp == IAPDU_Req_TAG)    ///Only Data In Buf
                        {
                            IsInfoFirstFrame = true;
                            byte[] t = data.ReadBytes((int)(data.BaseStream.Length - data.BaseStream.Position));
                            _APDU = t;
                        }
                        else
                        {
                            data.BaseStream.Position = (data.BaseStream.Position - 3);
                            _APDU = data.ReadBytes((int)(data.BaseStream.Length - data.BaseStream.Position));
                        }

                    }
                    else if (_FrameType == FrameType.I || _FrameType == FrameType.UI)
                    {
                        data.BaseStream.Position--;
                        _APDU = data.ReadBytes((int)(data.BaseStream.Length - data.BaseStream.Position));
                        IsInfoFirstFrame = false;
                    }
                    else if (curByte == 0x81)                               // Expected APDU SNRM|UA
                    {
                        uint tmp = 0;
                        tmp = curByte;
                        curByte = data.ReadByte();
                        tmp = (uint)(tmp | (uint)(curByte << 8));
                        if (tmp == SNRM_APDU_Req_TAG)                       // SNRM|UA TAG
                        {
                            curByte = data.ReadByte();                      // APDU length
                            APDUlength = curByte;
                            curByte = data.ReadByte();
                            if (curByte == APDU_Buf_TTAG)                   // Optional Information Buf Transmit Size
                            {
                                ushort infoBufSize = 0;
                                curByte = data.ReadByte();                  // length
                                if (curByte == 1 || curByte == 2)
                                {
                                    infoBufSize = data.ReadByte();
                                    if (curByte == 2)
                                        infoBufSize = (ushort)((infoBufSize << 8) | data.ReadByte());
                                    APDU_Buf_Transmit = infoBufSize;
                                }
                                else
                                    return false;

                                curByte = data.ReadByte();
                            }
                            else
                            {
                                this.APDU_Buf_Transmit = 0x128;
                            }
                            if (curByte == APDU_Buf_RTAG)                   // Optional Information Buf Receive Size
                            {
                                ushort infoBufSize = 0;
                                curByte = data.ReadByte();                  // length
                                if (curByte == 1 || curByte == 2)
                                {
                                    infoBufSize = data.ReadByte();
                                    if (curByte == 2)
                                        infoBufSize = (ushort)((infoBufSize << 8) | data.ReadByte());
                                    APDU_Buf_Receive = infoBufSize;
                                }
                                else
                                    return false;
                                curByte = data.ReadByte();                  // Transmit Window Tag
                            }
                            else
                            {
                                this.APDU_Buf_Receive = 0x128;
                            }
                            if (curByte == WinSize_TTAG)
                            {
                                int length = data.ReadByte();               // data length
                                UInt32 winTSize = 0;
                                if (length <= 0)
                                    return false;
                                for (int count = 1; count <= length; count++)
                                {
                                    winTSize = (uint)(winTSize << ((count - 1) * 8) | (data.ReadByte()));
                                }
                                WinSizeTransmit = (byte)(winTSize % 8);
                            }
                            else
                                return false;
                            curByte = data.ReadByte();                      // Transmit Window Tag
                            if (curByte == WinSize_RTAG)
                            {
                                int length = data.ReadByte();                // data length
                                UInt32 winTSize = 0;
                                if (length <= 0)
                                    return false;
                                for (int count = 1; count <= length; count++)
                                {
                                    winTSize = (uint)(winTSize << ((count - 1) * 8) | (data.ReadByte()));
                                }
                                WinSizeReceive = (byte)(winTSize % 8);
                            }
                            else
                                return false;

                        }
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // LOG Exception
                return false;
            }
        }

        public bool TryParseAPDU(byte[] APDU)
        {
            if (APDU != null)
                return TryParseAPDU(APDU, 0, APDU.Length);
            else
                return false;
        }

        public bool Parse(byte[] buffer, byte[] _rawDstAddress = null, byte[] _rawSourceAddress = null)
        {
            try
            {
                long headerStartIndex = 0;
                long APDUStartIndex = 0;
                long APDULength = 0;
                byte curByte = 0;

                using (BinaryReader data = new BinaryReader(new MemoryStream(buffer, false)))
                {
                    curByte = data.ReadByte();                                          // Frame Startup Tag
                    if (curByte != FrameTag)
                        throw new HDLCInValidFrameException("Invalid Frame,Frame Startup Tag 0x7E");

                    ushort _t = 0;
                    headerStartIndex = data.BaseStream.Position;
                    _t = data.ReadByte();                                                // Reading Frame Format    
                    _t = (ushort)((_t << 8) | data.ReadByte());
                    FrameFormat = _t;
                    List<byte> address = new List<byte>(4);
                    // Reading Destination Address
                    for (int count = 1; count <= 4; count++)
                    {
                        curByte = data.ReadByte();
                        bool isEven = (curByte % 2 == 0);
                        if (isEven && (count == 1 || count == 2 || count == 3))             // Even Byte                    
                        {
                            address.Insert(0, curByte);
                        }
                        else if (isEven && count == 4)
                        {
                            throw new HDLCInValidFrameException("Invalid Frame,Invalid Destination Address");
                        }
                        else if (!isEven && (count == 1 || count == 2 || count == 4))
                        {
                            address.Insert(0, curByte);                                     // Address Complete
                            break;
                        }
                        else
                        {
                            throw new HDLCInValidFrameException("Invalid Frame,Invalid Destination Address");
                        }
                    }
                    DestinationHDLCAddress = address.ToArray();
                    // Verify HDLC Destination Address
                    if ( _rawSourceAddress != null)
                    {
                        bool isVerify = false;

                        if (DestinationHDLCAddress != null &&
                           DestinationHDLCAddress.Length == _rawSourceAddress.Length)
                        {
                            isVerify = DestinationHDLCAddress.IsEquals(_rawSourceAddress);
                        }
                        // Verify Using HDLC Frame Function
                        else
                        {
                            ushort Addr1_Up = 0, Addr1_Low = 0;
                            ushort Addr2_Up = 0, Addr2_Low = 0;

                            Addr1_Low = GetLowerHDLCAddress(ref _destinationAddr);
                            Addr1_Up = GetUpperHDLCAddress(ref _destinationAddr);

                            Addr2_Low = GetLowerHDLCAddress(ref _rawSourceAddress);
                            Addr2_Up = GetUpperHDLCAddress(ref _rawSourceAddress);

                            isVerify = Addr1_Low == Addr2_Low && Addr1_Up == Addr2_Up;
                        }
                        
                        if (!isVerify)
                            return isVerify;
                    }
                    else if (!verifyAddress(DestinationHDLCAddress))
                        throw new HDLCInValidFrameException("Invalid Frame,Invalid Destination Address");

                    address.Clear();
                    for (int count = 1; count <= 4; count++)                                    // Source Address
                    {
                        curByte = data.ReadByte();
                        bool isEven = (curByte % 2 == 0);
                        if (isEven && (count == 1 || count == 2 || count == 3))               // Even Byte                    
                        {
                            address.Insert(0, curByte);
                        }
                        else if (isEven && count == 4)
                        {
                            throw new HDLCInValidFrameException("Invalid Frame,Invalid Source Address");
                        }
                        else if (!isEven && (count == 1 || count == 2 || count == 4))
                        {
                            address.Insert(0, curByte);                                     // Address Complete
                            break;
                        }
                        else
                        {
                            throw new HDLCInValidFrameException("Invalid Frame,Invalid Source Address");
                        }
                    }
                    SourceHDLCAddress = address.ToArray();

                    // Verify HDLC Source Address
                    if (_rawDstAddress != null)
                    {
                        bool isVerify = false;

                        if (SourceHDLCAddress != null &&
                            SourceHDLCAddress.Length == _rawDstAddress.Length)
                        {
                            isVerify = SourceHDLCAddress.IsEquals(_rawDstAddress);
                        }
                        // Verify Using HDLC Frame Function
                        else
                        {
                            ushort Addr1_Up = 0, Addr1_Low = 0;
                            ushort Addr2_Up = 0, Addr2_Low = 0;

                            Addr1_Low = GetLowerHDLCAddress(ref _sourceAddr);
                            Addr1_Up = GetUpperHDLCAddress(ref _sourceAddr);

                            Addr2_Low = GetLowerHDLCAddress(ref _rawDstAddress);
                            Addr2_Up = GetUpperHDLCAddress(ref _rawDstAddress);

                            isVerify = Addr1_Low == Addr2_Low && Addr1_Up == Addr2_Up;
                        }

                        if (!isVerify)
                            return isVerify;
                    }
                    else if (!verifyAddress(DestinationHDLCAddress))
                        throw new HDLCInValidFrameException("Invalid Frame,Invalid Source Address");

                    ControlField = data.ReadByte();                                             // Control Field
                    ushort _hcsCalc = CheckSum.Calc_CRC16(buffer, (int)headerStartIndex, (int)(data.BaseStream.Position - headerStartIndex));
                    ushort _hcsRecv = 0;
                    _hcsRecv = data.ReadByte();
                    _hcsRecv = (ushort)(_hcsRecv | (data.ReadByte() << 8));
                    if (_hcsRecv != _hcsCalc)                                                   // Invalid Header CRC
                    {
                        throw new HDLCInValidFrameException("Invalid Frame,Invalid Frame Header CRC");
                    }
                    else
                        _hcs = _hcsRecv;
                    bool APDUFlag = false;
                    if (_FrameType == FrameType.I || _FrameType == FrameType.UI)
                    // Process Application PayLoad
                    {
                        APDUStartIndex = data.BaseStream.Position;
                        APDULength = FrameLength - (APDUStartIndex + 1);
                        if (!TryParseAPDU(buffer, (int)APDUStartIndex, (int)APDULength))         ///Error Parsing APDU
                            throw new HDLCInValidFrameException("Invalid Frame,Invalid Application Protocol Unit");
                        data.BaseStream.Position = APDUStartIndex + APDULength;
                        APDUFlag = true;
                    }
                    else if (_FrameType == FrameType.SNRM || _FrameType == FrameType.UA)
                    {
                        APDUStartIndex = data.BaseStream.Position;
                        APDULength = FrameLength - (APDUStartIndex + 1);
                        if (APDULength > 0)
                        {
                            ushort t = data.ReadUInt16();
                            if (t == SNRM_APDU_Req_TAG) /// Frame Contains APDU 
                            {
                                APDUFlag = true;
                                data.BaseStream.Position -= 2;
                                if (!TryParseAPDU(buffer, (int)APDUStartIndex, (int)APDULength))         /// Error Parsing APDU
                                    throw new HDLCInValidFrameException("Invalid Frame,Invalid Application Protocol Unit");
                                data.BaseStream.Position = APDUStartIndex + APDULength;
                            }
                            else
                                data.BaseStream.Position -= 2;
                        }
                        else
                        {
                            // Set Flags to Zero
                            WinSizeT = 0;
                            WinSizeR = 0;
                            APDU_Buf_R = 0;
                            APDU_Buf_T = 0;

                            APDUFlag = false;
                        }
                    }
                    if (APDUFlag)   // If APDU Attached,Verify FCS
                    {
                        ushort _fcsCalc = CheckSum.Calc_CRC16(buffer, (int)headerStartIndex, (int)(data.BaseStream.Position - headerStartIndex));
                        ushort _fcsRecv = 0;
                        _fcsRecv = data.ReadByte();
                        curByte = data.ReadByte();
                        _fcsRecv = (ushort)(_fcsRecv | (curByte << 8));
                        if (_fcsRecv != _fcsCalc)                                                 /// Invalid Frame CRC
                        {
                            throw new HDLCInValidFrameException("Invalid Frame,Invalid Frame CRC");
                        }
                        else
                            _fcs = _fcsRecv;
                        curByte = data.ReadByte();                                              /// Frame End Tag
                        if (curByte != FrameTag)
                            throw new HDLCInValidFrameException("Invalid Frame,Frame Tag 0x7E");
                    }
                    // Verify Frame Length Later

                }

                return true;
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(HDLCInValidFrameException))
                    throw ex;
                else
                    throw new HDLCInValidFrameException("Invalid HDLC Frame", ex);
            }
        }

        public bool TryParse(byte[] buffer)
        {
            try
            {
                Parse(buffer);
                return true;
            }
            catch (Exception ex)
            {

                if (ex.GetType() == typeof(HDLCInValidFrameException))
                {
                    ///Logger 
                    errorMessage = ex.Message;
                }
                else
                {
                    ///Logger
                    errorMessage = ex.Message;
                }
                return false;
            }
        }

        public override string ToString()
        {
            try
            {
                StringBuilder retVal = new StringBuilder();

                retVal.AppendFormat("Frame Type:{0},", _FrameType);
                retVal.AppendFormat("Poll/Final:{0},", P_F);
                retVal.AppendFormat("Frame Length:0x{0:X},", FrameLength);
                retVal.AppendFormat("Is Segmented:{0},", IsSegmented);
                retVal.AppendFormat("Destination HDLC Address:Upper:Lower 0x{0:X}:0x{1:X},", UpperHDLCDestAddress, LowerHDLCDestAddress);
                retVal.AppendFormat("Source HDLC Address:Upper:Lower 0x{0:X}:0x{1:X},", UpperHDLCSourceAddress, LowerHDLCSourceAddress);
                retVal.AppendFormat("HCS:0x{0:X},", HCS);
                retVal.AppendFormat("FCS:0x{0:X},", FCS);

                return retVal.ToString();
            }
            catch (Exception ex)
            {
                return base.ToString() + String.Format("<<Error:{0}>>>", ex.Message);
            }
        }

        public string ToString(bool HexOrText)
        {
            try
            {
                if (HexOrText)  // return Hex Format
                {
                    byte[] _frmr = ToByteArray();
                    StringBuilder retVal = new StringBuilder(FrameLength + 2);
                    foreach (byte Byte in _frmr)
                    {
                        retVal.AppendFormat(" {0:X}", Byte);
                    }
                    return retVal.ToString();
                }
                else
                {
                    return this.ToString();
                }
            }
            catch (Exception ex)
            {
                return base.ToString() + String.Format("<<Error:{0}>>>", ex.Message);
            }
        }

        #endregion

    }

    public enum FrameType : byte
    {  
        None = 0x00,
                        // Defines Only Fixed Bits & P/F bit Set 
        I = 0x10,       // Information Frame
        RR = 0x11,      // Ready To Receive Frame
        RNR = 0x15,     // Ready Not To Receive Frame
        SNRM = 0x93,    // Set Normal Response Mode Frame
        DISC = 0x53,    // Disconnect Frame
        UA = 0x73,      // Un-Numbered Ack Frame
        DM = 0x1F,      // Disconnected Mode Frame
        FRMR = 0x97,    // Frame Reject Response Frame
        UI = 0x13       // Un-Numbered Information
    }

    public enum AddressLength : byte
    {
        /// <summary>
        /// One byte HDLC addressing scheme
        /// </summary>
        One_Byte = 1,
        /// <summary>
        /// Two byte HDLC addressing scheme
        /// </summary>
        Two_Byte = 2,
        /// <summary>
        /// Four byte HDLC addressing scheme
        /// </summary>
        Four_Byte = 4
    }

    public class HDLCInValidFrameException : Exception
    {
        public HDLCInValidFrameException() : base("Invalid HDLC Frame") { }
        public HDLCInValidFrameException(String Message) : base(Message) { }
        public HDLCInValidFrameException(String Message, Exception InnerException) : base(Message, InnerException) { }
    }
}
