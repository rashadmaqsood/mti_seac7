using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLMS;
using DLMS.Comm;

namespace DLMS
{

    #region Class_28

    /// <summary>
    /// The Class_28 “Auto Answer” new capabilities are added to manage wake-up requests that may be in the form 
    /// of a wake-up call or a wake-up message e.g. an (empty) SMS message. 
    /// After a successful wake-up request, the device connects to the network.
    /// </summary>
    public class Class_28 : Base_Class
    {

        #region Data Members

        public const int DefaultNumberOfCalls = 00;

        /// <summary>
        /// Defines the working mode of the line when the device is auto answering
        /// </summary>
        public AutoAnswerMode Mode;

        /// <summary>
        /// Contains the start and end date/time stamp when the window becomes active.
        /// </summary>
        public List<KeyValuePair<StDateTime, StDateTime>> ListeningWindow = null;

        /// <summary>
        /// The AutoAnswerStatus field notify either device allows any wakeup call or not at present. 
        /// Either service is blocked temporarly
        /// </summary>
        public AutoAnswerStatus Status;

        /// <summary>
        /// This number is the reference used in modes 1 and 2.When set to 0, this means there is no limit.
        /// </summary>;
        public int NumberOfCalls;

        /// <summary>
        /// Number of rings within the window defined by ListeningWindow.        
        /// </summary>
        public int NumberOfRingsInListeningWindow;

        /// <summary>
        /// Number of rings outside the window defined by ListeningWindow.        
        /// </summary>
        public int NumberOfRingsOutListeningWindow;

        /// <summary>
        /// Contains an - optional - list of Calling numbers which further limits the connectivity of the modem based on the calling number. 
        /// It also controls the acceptance of wake-up calls or wake-up messages (e.g. SMS) from a calling number.
        /// This requires the presence of a calling line identification (CLI) service in the communication network used.
        /// </summary>
        public List<KeyValuePair<AutoAnswerCallerType, byte[]>> Allowed_AutoAnswerCallers = null;

        #endregion

        #region Constructor

        public Class_28(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(28, 07, 00, Index, Obis_Code, No_of_Associations)
        {
            // make room
            Mode = AutoAnswerMode.UnlimitedAutoDial;
            NumberOfCalls = DefaultNumberOfCalls;

            // null call window
            ListeningWindow = new List<KeyValuePair<StDateTime, StDateTime>>(5);
            NumberOfRingsInListeningWindow = NumberOfRingsOutListeningWindow = DefaultNumberOfCalls;

            // null Allowed Wakeup List
            Allowed_AutoAnswerCallers = new List<KeyValuePair<AutoAnswerCallerType, byte[]>>(05);
        }

        public Class_28(byte[] Obis_Code, byte Attribute_recieved)
            : base(28, 07, 00, Obis_Code)
        {
            // make room
            Mode = AutoAnswerMode.UnlimitedAutoDial;
            NumberOfCalls = DefaultNumberOfCalls;

            // null call window
            ListeningWindow = new List<KeyValuePair<StDateTime, StDateTime>>(5);
            NumberOfRingsInListeningWindow = NumberOfRingsOutListeningWindow = DefaultNumberOfCalls;

            // null Allowed Wakeup List
            Allowed_AutoAnswerCallers = new List<KeyValuePair<AutoAnswerCallerType, byte[]>>(05);
        }

        public Class_28(StOBISCode OBISCodeStruct)
            : base(OBISCodeStruct, 07, 00)
        {
            // make room
            Mode = AutoAnswerMode.UnlimitedAutoDial;
            NumberOfCalls = DefaultNumberOfCalls;

            // null call window
            ListeningWindow = new List<KeyValuePair<StDateTime, StDateTime>>(5);
            NumberOfRingsInListeningWindow = NumberOfRingsOutListeningWindow = DefaultNumberOfCalls;

            // null Allowed Wakeup List
            Allowed_AutoAnswerCallers = new List<KeyValuePair<AutoAnswerCallerType, byte[]>>(05);
        }

        /// <summary>
        /// Copy Constructor Class_28
        /// </summary>
        /// <param name="obj"></param>
        public Class_28(Class_28 obj)
            : base(obj)
        {
            // make room
            Mode = obj.Mode;
            Status = obj.Status;
            NumberOfCalls = obj.NumberOfCalls;

            NumberOfRingsInListeningWindow = obj.NumberOfRingsInListeningWindow;
            NumberOfRingsOutListeningWindow = obj.NumberOfRingsOutListeningWindow;

            if (obj.ListeningWindow != null &&
                obj.ListeningWindow.Count > 0)
            {
                ListeningWindow = new List<KeyValuePair<StDateTime, StDateTime>>(5);

                foreach (var calWin in obj.ListeningWindow)
                {
                    KeyValuePair<StDateTime, StDateTime> win = new KeyValuePair<StDateTime, StDateTime>
                        (new StDateTime(calWin.Key), new StDateTime(calWin.Value));

                    ListeningWindow.Add(win);
                }
            }
            // null call window
            else
                ListeningWindow = new List<KeyValuePair<StDateTime, StDateTime>>(5);

            if (obj.Allowed_AutoAnswerCallers != null &&
                obj.Allowed_AutoAnswerCallers.Count > 0)
            {
                Allowed_AutoAnswerCallers = new List<KeyValuePair<AutoAnswerCallerType, byte[]>>(04);

                foreach (var dstItem in obj.Allowed_AutoAnswerCallers)
                {
                    KeyValuePair<AutoAnswerCallerType, byte[]> caller;
                    byte[] Val = null;

                    if (dstItem.Value == null || dstItem.Value.Length <= 0)
                        Val = null;
                    else
                        Val = (byte[])dstItem.Value.Clone();

                    caller = new KeyValuePair<AutoAnswerCallerType, byte[]>(dstItem.Key, Val);
                    Allowed_AutoAnswerCallers.Add(caller);
                }
            }
            else
                // null destination
                Allowed_AutoAnswerCallers = new List<KeyValuePair<AutoAnswerCallerType, byte[]>>(04);
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
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_28_Auto_Answer");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_28_Auto_Answer");
                //------------------------------------------------------
                #region Attribute 0x02 AutoAnswer Mode

                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check access rights
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
                            Mode = (AutoAnswerMode)(Convert.ToByte(_val));
                            SetAttributeDecodingResult(2, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                            throw new DLMSDecodingException("This element is not 16 (enum)", "Decode_Data_Class_28_Auto_Answer", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x03 Listening_window

                if (DecodingAttribute == 0x03 ||
                    DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check access rights
                        if (!IsAttribReadable(0x03))
                            SetAttributeDecodingResult(0x03, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x03, DecodingResult.DecodingError);
                    }
                    else
                    {
                        if (ListeningWindow == null)
                            // null call window
                            ListeningWindow = new List<KeyValuePair<StDateTime, StDateTime>>(04);
                        else
                            ListeningWindow.Clear();

                        try
                        {
                            if (current_char != (byte)DataTypes._A01_array)
                                throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode Array Structure,Null Reference", OBISIndex, OBISIndex.OBISIndex),
                                                                "Decode_Data_Class_28_Auto_Answer");

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
                                    ListeningWindow.Add(Dt_Value);
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
                                    ListeningWindow.Add(Dt_Value);
                                }
                                else
                                    throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode Listening Window Structure,Invalid Structure",
                                                                    OBISIndex, OBISIndex.OBISIndex), "Decode_Data_Class_28_Auto_Answer");
                            }

                            SetAttributeDecodingResult(0x03, DecodingResult.Ready);
                        }
                        catch (DLMSDecodingException ex)
                        {
                            SetAttributeDecodingResult(0x03, DecodingResult.DecodingError);
                            throw ex;
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(0x03, DecodingResult.DecodingError);
                            throw new DLMSDecodingException("Invalid Data Format Calling Windows", "Decode_Data_Class_28_Auto_Answer", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x04 Status

                if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check access rights
                        if (!IsAttribReadable(0x04))
                            SetAttributeDecodingResult(0x04, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x04, DecodingResult.DecodingError);
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            var _val = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length);
                            Status = (AutoAnswerStatus)(Convert.ToByte(_val));
                            SetAttributeDecodingResult(0x04, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(0x04, DecodingResult.DecodingError);
                            throw new DLMSDecodingException("This element is not 16 (enum)", "Decode_Data_Class_28_Auto_Answer", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x05 Number Of Calls

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
                        try
                        {
                            array_traverse--;
                            var _val = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length);
                            NumberOfCalls = (Convert.ToInt32(_val));
                            SetAttributeDecodingResult(0x05, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(0x05, DecodingResult.DecodingError);
                            throw new DLMSDecodingException("This element is not (unsigned)", "Decode_Data_Class_28_Auto_Answer", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x06 Number Of Rings

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
                        try
                        {
                            // Null Call Window Entry
                            if (!(Data[array_traverse] == (byte)DataTypes._A02_structure &&
                                Data[array_traverse++] == 0x02))
                                throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode Number Of Rings,Invalid Structure", OBISIndex, OBISIndex.OBISIndex),
                                                                "Decode_Data_Class_28_Auto_Answer");

                            ValueType _val = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length);
                            NumberOfRingsInListeningWindow = (Convert.ToInt32(_val));

                            _val = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length);
                            NumberOfRingsOutListeningWindow = (Convert.ToInt32(_val));

                            SetAttributeDecodingResult(0x06, DecodingResult.Ready);
                        }
                        catch (DLMSDecodingException ex)
                        {
                            SetAttributeDecodingResult(0x06, DecodingResult.DecodingError);
                            throw ex;
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(0x06, DecodingResult.DecodingError);
                            throw new DLMSDecodingException("This element is not (Structure)", "Decode_Data_Class_28_Auto_Answer", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x07 List Of Allowd Callers

                if (DecodingAttribute == 0x07 ||
                    DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(0x07))
                            SetAttributeDecodingResult(0x07, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(0x07, DecodingResult.DecodingError);
                    }
                    else
                    {
                        if (Allowed_AutoAnswerCallers == null)
                            // null call window
                            Allowed_AutoAnswerCallers = new List<KeyValuePair<AutoAnswerCallerType, byte[]>>(04);
                        else
                            Allowed_AutoAnswerCallers.Clear();

                        try
                        {
                            if (current_char != (byte)DataTypes._A01_array)
                                throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode Array Structure,Null Reference", OBISIndex, OBISIndex.OBISIndex),
                                                                "Decode_Data_Class_28_Auto_Answer");

                            int arrlength = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);

                            KeyValuePair<AutoAnswerCallerType, byte[]> Dst_Value;
                            byte[] Caller_ID_Value = null;
                            AutoAnswerCallerType Caller_Type;

                            for (int Id = 1; Id <= arrlength; Id++)
                            {
                                Caller_ID_Value = null;
                                Caller_Type = AutoAnswerCallerType.WakeupCallsOnly;

                                current_char = Data[array_traverse++];
                                // Null Caller Entry
                                if (current_char == (byte)DataTypes._A00_Null)
                                {
                                    // null value
                                    Dst_Value = new KeyValuePair<AutoAnswerCallerType, byte[]>(AutoAnswerCallerType.WakeupCallsOnly, null);
                                    Allowed_AutoAnswerCallers.Add(Dst_Value);
                                }
                                else if (current_char == (byte)DataTypes._A02_structure &&
                                         Data[array_traverse++] == 0x02)
                                {
                                    // Caller Id
                                    Caller_ID_Value = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
                                    var _val = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length);
                                    Caller_Type = (AutoAnswerCallerType)(Convert.ToByte(_val));

                                    Dst_Value = new KeyValuePair<AutoAnswerCallerType, byte[]>(Caller_Type, Caller_ID_Value);
                                    Allowed_AutoAnswerCallers.Add(Dst_Value);
                                }
                            }

                            SetAttributeDecodingResult(0x07, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(0x07, DecodingResult.DecodingError);
                            throw new DLMSDecodingException("Invalid Data Format Allowed Caller List", "Decode_Data_Class_28_Auto_Answer", ex);
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
                    throw new DLMSDecodingException("Error occurred while decoding", "Decode_Data_Class_28_Auto_Answer", ex);
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
                #region Attribute 0x02 AutoAnswer Mode

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
                        throw new DLMSEncodingException(String.Format("Unable to encode Auto Answer Mode Enum,No Access", EncodingType)
                                                        , "Encode_Data_Class_28_Auto_Answer");
                    }
                    else  // Encode Here Data Mode
                    {
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A16_enum, (byte)this.Mode));
                    }
                }

                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x03 Listening Windows

                if (EncodingAttribute == 0x03 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x03);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException("Unable to encode Listening Windows, No Access", "Encode_Data_Class_28_Auto_Answer");
                    }
                    else  // Encode Here Data Repetition Delay <<DataType Array Of Struct>>
                    {
                        // null Value
                        if (ListeningWindow == null || ListeningWindow.Count <= 0)
                        {
                            EncodedRaw.Add((byte)DataTypes._A00_Null);
                        }
                        else
                        {
                            byte[] T_Array = null;

                            BasicEncodeDecode.Encode_Length(ref T_Array, Convert.ToUInt16(ListeningWindow.Count));

                            EncodedRaw.Add((byte)DataTypes._A01_array);
                            EncodedRaw.AddRange(T_Array);

                            foreach (var calWin in ListeningWindow)
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
                #region Attribute 0x04 AutoAnswer Status

                if (EncodingAttribute == 0x04 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x04);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException("Unable to Encode AutoAnswer Status,No Access", "Encode_Data_Class_28_Auto_Answer");
                    }
                    else  // Encode Here Data Repetitions <<DataType _A16_enum>>
                    {
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A16_enum, Convert.ToByte(this.Status)));
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x05 Number OF Calls

                if (EncodingAttribute == 0x05 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x05);
                    if (EncodingAttribute == 0x00 &&
                        !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException("Unable to encode Number Of Calls,No Access", "Encode_Data_Class_28_Auto_Answer");
                    }
                    else  // Encode Here Data Number of Calls <<DataType _A11_unsigned>>
                    {
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, Convert.ToUInt16(this.NumberOfCalls)));
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x06 Number OF Rings

                if (EncodingAttribute == 0x06 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x06);
                    if (EncodingAttribute == 0x00 &&
                        !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException("Unable to encode Number Of Rings,No Access", "Encode_Data_Class_28_Auto_Answer");
                    }
                    else
                    {
                        EncodedRaw.Add((byte)DataTypes._A02_structure);
                        EncodedRaw.Add(0x02);

                        // Encode Here Data Number Of Rings In ListeningWindow <<DataType _A11_unsigned>>
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, Convert.ToUInt16(this.NumberOfRingsInListeningWindow)));
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, Convert.ToUInt16(this.NumberOfRingsOutListeningWindow)));
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x07 Allowed Caller List

                if (EncodingAttribute == 0x07 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x07);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException("Unable to encode Allowed Caller List,No Access", "Encode_Data_Class_28_Auto_Answer");
                    }
                    else  // Encode Here Data Destination List <<Caller Id, Caller Type>>
                    {
                        // null Value
                        if (Allowed_AutoAnswerCallers == null || Allowed_AutoAnswerCallers.Count <= 0)
                        {
                            EncodedRaw.Add((byte)DataTypes._A00_Null);
                        }
                        else
                        {
                            byte[] T_Array = null;

                            BasicEncodeDecode.Encode_Length(ref T_Array, Convert.ToUInt16(Allowed_AutoAnswerCallers.Count));

                            EncodedRaw.Add((byte)DataTypes._A01_array);
                            EncodedRaw.AddRange(T_Array);

                            foreach (var caller in Allowed_AutoAnswerCallers)
                            {
                                // null Caller Window Entry
                                if (caller.Value == null || caller.Value.Length <= 0 || caller.Key == null)
                                {
                                    EncodedRaw.Add((byte)DataTypes._A00_Null);
                                }
                                else
                                {
                                    EncodedRaw.Add((byte)DataTypes._A02_structure);
                                    EncodedRaw.Add(0x02);

                                    byte[] encodedData = null;

                                    // Caller ID Value
                                    encodedData = BasicEncodeDecode.Encode_OctetString(caller.Value, DataTypes._A09_octet_string);

                                    EncodedRaw.AddRange(encodedData);

                                    // Caller Type
                                    EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A16_enum, Convert.ToUInt16(caller.Key)));
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
                    throw new DLMSException(String.Format("{0}_{1}", "Error occurred while encoding data", "Encode_Data_Class_28_Auto_Answer"), ex);
            }
        }

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_28 cloned = new Class_28(this);
            return cloned;
        }

        public override string ToString()
        {
            String baseStr = base.ToString();
            StringBuilder strVal = new StringBuilder();

            strVal.AppendFormat(",Mode:{0} :{1}", Mode, GetAttributeDecodingResult(02));
            strVal.AppendFormat(",Listening Windows Count:{0}:{1}", (ListeningWindow != null) ? ListeningWindow.Count : 0, GetAttributeDecodingResult(03));
            strVal.AppendFormat(",Status :{0}:{1}", Status, GetAttributeDecodingResult(04));
            strVal.AppendFormat(",Number Of Calls :{0}:{1}", NumberOfCalls, GetAttributeDecodingResult(05));
            strVal.AppendFormat(", Allowed Callers List:{0}:{1}", (Allowed_AutoAnswerCallers != null) ? Allowed_AutoAnswerCallers.Count : 0, GetAttributeDecodingResult(07));

            // Comment to Allowed Callers List
            if (Allowed_AutoAnswerCallers != null &&
                Allowed_AutoAnswerCallers.Count > 0)
            {
                int dst_Id = 1;
                foreach (var dst in Allowed_AutoAnswerCallers)
                {
                    string CallerNumber = string.Empty;
                    if (dst.Value != null && dst.Value.Length > 0)
                        CallerNumber = ASCIIEncoding.ASCII.GetString(dst.Value);

                    strVal.AppendFormat("{0} {1} \r\n", dst_Id, CallerNumber);
                }
            }

            return baseStr + strVal;
        }

        #endregion
    }

    #endregion
}
