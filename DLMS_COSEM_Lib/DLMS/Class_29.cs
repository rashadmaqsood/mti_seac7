using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLMS;
using DLMS.Comm;

namespace DLMS
{
    #region Class_29

    /// <summary>
    /// The Class_29 “Auto connect” class models how the device performs auto dialling or sends messages using various services.
    /// Auto Connect implements data transfer from the device to one or several destinations.
    /// </summary>
    public class Class_29 : Base_Class
    {
        /// <summary>
        /// Manual Connection Method to Invoke
        /// </summary>
        public const byte ManualConnect = 1;

        #region Data Members

        public const int DefaultConnectionDelay = 02;

        /// <summary>
        /// Defines the mode controlling the auto dial functionality concerning the
        /// timing, the message type to be sent and the infrastructure to be used.
        /// </summary>
        public AutoConnectMode Mode;

        /// <summary>
        /// The maximum number of trials in the case of unsuccessful dialling attempts.
        /// </summary>;
        public int Repetitions;

        /// <summary>
        /// The time delay, expressed in seconds until an unsuccessful dial attempt can be repeated.
        /// </summary>
        public int RepetitionDelay;

        /// <summary>
        /// Contains the start and end date/time stamp when the window becomes active.
        /// </summary>
        public List<KeyValuePair<StDateTime, StDateTime>> CallingWindow = null;

        /// <summary>
        /// Contains the list of destinations (for example phone numbers, email 
        /// addresses or their combinations) where the message(s) have to be sent 
        /// under certain conditions. The conditions and their link to the elements of 
        /// the array are not defined here.
        /// </summary>
        public List<byte[]> Destinations = null;

        #endregion

        #region Constructor

        public Class_29(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(29, 06, 01, Index, Obis_Code, No_of_Associations)
        {
            // make room
            Mode = AutoConnectMode.NoAutoDialling;
            Repetitions = 1;
            RepetitionDelay = DefaultConnectionDelay;
            // null call window
            CallingWindow = new List<KeyValuePair<StDateTime, StDateTime>>(5);
            // null destination
            Destinations = new List<byte[]>(04);
        }

        public Class_29(byte[] Obis_Code, byte Attribute_recieved)
            : base(29, 06, 01, Obis_Code)
        {
            DecodingAttribute = Attribute_recieved;

            // make room
            Mode = AutoConnectMode.NoAutoDialling;
            Repetitions = 1;
            RepetitionDelay = DefaultConnectionDelay;
            // null call window
            CallingWindow = new List<KeyValuePair<StDateTime, StDateTime>>(5);
            // null destination
            Destinations = new List<byte[]>(04);
        }

        public Class_29(StOBISCode OBISCodeStruct)
            : base(OBISCodeStruct, 06, 01)
        {
            // make room
            Mode = AutoConnectMode.NoAutoDialling;
            Repetitions = 1;
            RepetitionDelay = DefaultConnectionDelay;
            // null call window
            CallingWindow = new List<KeyValuePair<StDateTime, StDateTime>>(05);
            // null destination
            Destinations = new List<byte[]>(04);
        }

        /// <summary>
        /// Copy Constructur Class_29
        /// </summary>
        /// <param name="obj"></param>
        public Class_29(Class_29 obj)
            : base(obj)
        {
            // make room
            Mode = obj.Mode;
            Repetitions = obj.Repetitions;
            RepetitionDelay = obj.RepetitionDelay;

            if (obj.CallingWindow != null &&
                obj.CallingWindow.Count > 0)
            {
                CallingWindow = new List<KeyValuePair<StDateTime, StDateTime>>(5);

                foreach (var calWin in obj.CallingWindow)
                {
                    KeyValuePair<StDateTime, StDateTime> win = new KeyValuePair<StDateTime, StDateTime>
                        (new StDateTime(calWin.Key), new StDateTime(calWin.Value));

                    CallingWindow.Add(win);
                }
            }
            // null call window
            else
                CallingWindow = new List<KeyValuePair<StDateTime, StDateTime>>(5);

            if (obj.Destinations != null &&
                obj.Destinations.Count > 0)
            {
                Destinations = new List<byte[]>(04);

                foreach (var dstItem in obj.Destinations)
                {
                    byte[] Arr = null;
                    Arr = dstItem.Clone() as byte[];

                    Destinations.Add(Arr);
                }
            }
            else
                // null destination
                Destinations = new List<byte[]>(04);
        }

        #endregion

        #region Decoders/Encoders

        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            byte[] Obis_code_recieved = null;
            byte current_char;
            //------------------------------------------------------
            if (DecodingAttribute == 0x00)
            {
                for (int index = 0; index < AccessResults.Length; index++)
                    AccessResults[index] = DecodingResult.DataNotPresent;
            }
            else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
            {
                SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
            }

            try
            {
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_29_Auto_Connect");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_29_Auto_Connect");
                //------------------------------------------------------
                #region Attribute 0x02 AutoConnect Mode

                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        //Check access rights
                        if (!IsAttribReadable(2))
                            SetAttributeDecodingResult(2, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            var _val = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length);
                            Mode = (AutoConnectMode)(Convert.ToByte(_val));
                            SetAttributeDecodingResult(2, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                            throw new DLMSDecodingException("This element is not 16 (enum)", "Decode_Data_Class_29_Auto_Connect", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x03 Repetitions

                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check access rights
                        if (!IsAttribReadable(03))
                            SetAttributeDecodingResult(03, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(03, DecodingResult.DecodingError);
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            var _val = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length);
                            Repetitions = (Convert.ToInt32(_val));
                            SetAttributeDecodingResult(03, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(03, DecodingResult.DecodingError);
                            throw new DLMSDecodingException("This element is not 16 (unsigned)", "Decode_Data_Class_29_Auto_Connect", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x04 Repetitions Dealy

                if (DecodingAttribute == 0x04 ||
                    DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check access rights
                        if (!IsAttribReadable(04))
                            SetAttributeDecodingResult(04, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(04, DecodingResult.DecodingError);
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            var _val = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length);
                            RepetitionDelay = (Convert.ToInt32(_val));
                            SetAttributeDecodingResult(04, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(04, DecodingResult.DecodingError);
                            throw new DLMSDecodingException("This element is not 16 (long-unsigned)", "Decode_Data_Class_29_Auto_Connect", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x05 Calling_window

                if (DecodingAttribute == 0x05 ||
                    DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check access rights
                        if (!IsAttribReadable(0x05))
                            SetAttributeDecodingResult(0x05, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x05, DecodingResult.DecodingError);
                    }
                    else
                    {
                        if (CallingWindow == null)
                            // null call window
                            CallingWindow = new List<KeyValuePair<StDateTime, StDateTime>>(05);
                        else
                            CallingWindow.Clear();

                        try
                        {
                            if (current_char != (byte)DataTypes._A01_array)
                                throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode Array Structure,Null Reference", OBISIndex, OBISIndex.OBISIndex),
                                                "Decode_Data_Class_29_Auto_Connect");

                            int arrlength = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);

                            KeyValuePair<StDateTime, StDateTime> Dt_Value;
                            StDateTime winStartTime = null;
                            StDateTime winEndTime = null;

                            for (int windId = 1; windId <= arrlength; windId++)
                            {
                                // Null Call Window Entry
                                if (Data[array_traverse++] == (byte)DataTypes._A00_Null)
                                {
                                    winStartTime = null;
                                    winEndTime = null;

                                    Dt_Value = new KeyValuePair<StDateTime, StDateTime>(winStartTime, winEndTime);
                                    CallingWindow.Add(Dt_Value);
                                }
                                else if (Data[array_traverse] == (byte)DataTypes._A02_structure &&
                                        Data[array_traverse++] == 0x02)
                                {
                                    // Start Time
                                    byte[] Value_Array = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);

                                    winStartTime = new StDateTime();
                                    winStartTime.DecodeDateTime(Value_Array);

                                    // End Time
                                    Value_Array = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);

                                    winEndTime = new StDateTime();
                                    winEndTime.DecodeDateTime(Value_Array);

                                    Dt_Value = new KeyValuePair<StDateTime, StDateTime>(winStartTime, winEndTime);
                                    CallingWindow.Add(Dt_Value);
                                }
                                else
                                    throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode Call Window Structure,Invalid Structure",
                                                                    OBISIndex, OBISIndex.OBISIndex), "Decode_Data_Class_29_Auto_Connect");
                            }

                            SetAttributeDecodingResult(0x05, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(0x05, DecodingResult.DecodingError);
                            throw new DLMSDecodingException("Invalid Data Format Calling Windows", "Decode_Data_Class_29_Auto_Connect", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x06 Destination List

                if (DecodingAttribute == 0x06 ||
                    DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check access rights
                        if (!IsAttribReadable(0x06))
                            SetAttributeDecodingResult(0x06, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x06, DecodingResult.DecodingError);
                    }
                    else
                    {
                        if (Destinations == null)
                            // null call window
                            Destinations = new List<byte[]>(04);
                        else
                            Destinations.Clear();

                        try
                        {
                            if (current_char != (byte)DataTypes._A01_array)
                                throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode Array Structure,Null Reference", OBISIndex, OBISIndex.OBISIndex),
                                                "Decode_Data_Class_29_Auto_Connect");

                            int arrlength = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);

                            byte[] Dst_Value = null;

                            for (int Id = 1; Id <= arrlength; Id++)
                            {
                                // Null Call Window Entry
                                if (Data[array_traverse] == (byte)DataTypes._A00_Null)
                                {
                                    Dst_Value = null;
                                    Destinations.Add(Dst_Value);
                                }
                                else
                                {
                                    Dst_Value = null;
                                    // Address
                                    Dst_Value = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);

                                    Destinations.Add(Dst_Value);
                                }
                            }

                            SetAttributeDecodingResult(0x06, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(0x06, DecodingResult.DecodingError);
                            throw new DLMSDecodingException("Invalid Data Format Destination List", "Decode_Data_Class_29_Auto_Connect", ex);
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                {
                    throw new DLMSDecodingException("Error occurred while decoding", "Decode_Data_Class_29_Auto_Connect", ex);
                }
            }
        }

        public override byte[] Encode_Data()
        {
            try
            {
                EncodedRaw = new List<byte>(0x0A);
                //------------------------------------------------------
                EncoderAttribute_0();
                EncoderLogicalName();
                //------------------------------------------------------
                #region Attribute 0x02 AutoConnnect Mode

                if (EncodingAttribute == 0x02 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x02);
                    if (EncodingAttribute == 0x00 &&
                        !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Auto Connect Mode Enum,No Access", EncodingType)
                                                        , "Decode_Data_Class_29_Auto_Connect");
                    }
                    else  // Encode Here Data Mode
                    {
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A16_enum, (byte)this.Mode));
                    }
                }

                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x03 Repetitions

                if (EncodingAttribute == 0x03 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x03);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException("Unable to encode Repetitions,No Access", "Decode_Data_Class_29_Auto_Connect");
                    }
                    else  // Encode Here Data Repetitions <<DataType unsigned>>
                    {
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, Convert.ToByte(this.Repetitions)));
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x04 RepetitionDelay

                if (EncodingAttribute == 0x04 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x04);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException("Unable to encode RepetitionDelay,No Access", "Decode_Data_Class_29_Auto_Connect");
                    }
                    else  // Encode Here Data Repetition Delay <<DataType long_unsigned>>
                    {
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, Convert.ToUInt16(this.RepetitionDelay)));
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x05 CallingWindows

                if (EncodingAttribute == 0x05 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x05);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException("Unable to encode CallingWindows,No Access", "Decode_Data_Class_29_Auto_Connect");
                    }
                    else  // Encode Here Data Repetition Delay <<DataType Array Of Struct>>
                    {
                        // null Value
                        if (CallingWindow == null || CallingWindow.Count <= 0)
                        {
                            EncodedRaw.Add((byte)DataTypes._A00_Null);
                        }
                        else
                        {
                            byte[] T_Array = null;

                            BasicEncodeDecode.Encode_Length(ref T_Array, Convert.ToUInt16(CallingWindow.Count));

                            EncodedRaw.Add((byte)DataTypes._A01_array);
                            EncodedRaw.AddRange(T_Array);

                            foreach (var calWin in CallingWindow)
                            {
                                // null Call Win Entry
                                if (calWin.Key == null && calWin.Value == null)
                                {
                                    EncodedRaw.Add((byte)DataTypes._A00_Null);
                                }
                                else
                                {
                                    EncodedRaw.Add((byte)DataTypes._A02_structure);
                                    EncodedRaw.Add(0x02);

                                    // Win Start Time
                                    if (calWin.Key == null)
                                    {
                                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                                    }
                                    else
                                    {
                                        T_Array = calWin.Key.EncodeRawBytes();
                                        EncodedRaw.AddRange(T_Array);
                                    }

                                    // Win Ent Time
                                    // Win Start Time
                                    if (calWin.Value == null)
                                    {
                                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                                    }
                                    else
                                    {
                                        T_Array = calWin.Value.EncodeRawBytes();
                                        EncodedRaw.AddRange(T_Array);
                                    }
                                }
                            }
                        }


                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x06 Destination List

                if (EncodingAttribute == 0x06 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x06);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException("Unable to encode Destination List,No Access", "Decode_Data_Class_29_Auto_Connect");
                    }
                    else  // Encode Here Data Destination List <<DataType Array Of Octet Strings>>
                    {
                        // null Value
                        if (Destinations == null || Destinations.Count <= 0)
                        {
                            EncodedRaw.Add((byte)DataTypes._A00_Null);
                        }
                        else
                        {
                            byte[] T_Array = null;

                            BasicEncodeDecode.Encode_Length(ref T_Array, Convert.ToUInt16(Destinations.Count));

                            EncodedRaw.Add((byte)DataTypes._A01_array);
                            EncodedRaw.AddRange(T_Array);

                            foreach (var dst in Destinations)
                            {
                                // null Call Win Entry
                                if (dst == null || dst.Length <= 0)
                                {
                                    EncodedRaw.Add((byte)DataTypes._A00_Null);
                                }
                                else
                                {
                                    T_Array = BasicEncodeDecode.Encode_OctetString(dst, DataTypes._A09_octet_string);
                                    EncodedRaw.AddRange(T_Array);
                                }
                            }
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                byte[] dtBuf = EncodedRaw.ToArray<byte>();
                EncodedRaw = null;
                return dtBuf;
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSException(String.Format("{0}_{1}", "Error occurred while encoding data", "Encode_Data_Class_45_GPRS_Modem_SETUP"), ex);
            }
        }

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_29 cloned = new Class_29(this);
            return cloned;
        }

        public override string ToString()
        {
            String baseStr = base.ToString();
            StringBuilder strVal = new StringBuilder();

            strVal.AppendFormat(",Mode:{0} :{1}", Mode, GetAttributeDecodingResult(2));
            strVal.AppendFormat(",Repetition :{0}:{1}", Repetitions, GetAttributeDecodingResult(3));
            strVal.AppendFormat(",Repetition Delay :{0}:{1}", RepetitionDelay, GetAttributeDecodingResult(4));
            strVal.AppendFormat(",Call Window Count:{0}:{1}", (CallingWindow != null) ? CallingWindow.Count : 0, GetAttributeDecodingResult(5));
            strVal.AppendFormat(", Destination List:{0}:{1}", (Destinations != null) ? Destinations.Count : 0, GetAttributeDecodingResult(6));

            // Comment to Remove Destination Address
            if (Destinations != null &&
                Destinations.Count > 0)
            {
                int dst_Id = 1;
                foreach (var dst in Destinations)
                {
                    if (dst == null)
                        strVal.AppendFormat("{0} {1} \r\n", dst_Id, string.Empty);
                    else
                        strVal.AppendFormat("{0} {1} \r\n", dst_Id, DLMS_Common.ArrayToHexString(dst));
                }
            }

            return baseStr + strVal;
        }

        #endregion
    }

    #endregion
}
