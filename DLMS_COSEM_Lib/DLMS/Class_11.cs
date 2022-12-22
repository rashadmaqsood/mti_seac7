using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// Special days table Class (class_id: 11, version: 0) allows defining special dates. On such dates, a special switching behavior overrides the normal one.
    /// This class works in conjunction with the class Schedule or Activity_calendar. The linking data item is day_id.
    /// </summary>
    public class Class_11 : Base_Class
    {
        #region DataMembers
        
        private List<StSpecialDayProfile> specialDayProfiles;
        
        /// <summary>
        /// SpecialDayProfiles COSEM Interface Class 11 Attribute 0x02;Get or Set the collection of special days and their Time for profiling
        /// <see cref="StSpecialDayProfile"/>
        /// </summary>
        public List<StSpecialDayProfile> SpecialDayProfiles
        {
            get { return specialDayProfiles; }
            set { specialDayProfiles = value; }
        }

        #endregion

        #region Constructors
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_11(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(11, 2, 2, Index, Obis_Code, No_of_Associations)
        {
            ///Instantiate Class_11
            specialDayProfiles = new List<StSpecialDayProfile>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="Attribute_recieved">no of attribute associated with current object</param>
        public Class_11(byte[] Obis_Code, byte Attribute_recieved)
            : base(11, 2, 2, Obis_Code)
        {
            DecodingAttribute = Attribute_recieved;
            ///Instantiate Class_11
            specialDayProfiles = new List<StSpecialDayProfile>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCodeStruct">OBIS code for a specific Object</param>
        public Class_11(StOBISCode OBISCodeStruct)
            : base(OBISCodeStruct, 2, 2)
        {
            specialDayProfiles = new List<StSpecialDayProfile>();
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="obj">Class_11 Object</param>
        public Class_11(Class_11 obj)
            : base(obj)
        {
            if (obj.specialDayProfiles != null && obj.specialDayProfiles.Count > 0)
                specialDayProfiles = new List<StSpecialDayProfile>(obj.specialDayProfiles);
            else
                specialDayProfiles = new List<StSpecialDayProfile>();
        }

        #endregion

        #region Decoders/Encoders
        /// <summary>
        /// Decode Data of this Class which is received in response of get data Request
        /// </summary>
        /// <param name="Data">Recived data from Remote site</param>
        /// <param name="array_traverse">Off-Set</param>
        /// <param name="length">Length to decode</param>
        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            byte[] Obis_code_recieved = null;
            byte[] to_Compare_Array = null;
            byte current_char;
            //------------------------------------------------------
            try
            {
                ///SET All Attribute Access Status Results
                if (DecodingAttribute == 0x00)
                {
                    for (int index = 0; index < AccessResults.Length; index++)
                        AccessResults[index] = DecodingResult.DataNotPresent;
                }
                else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
                {
                    SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
                }
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_11_SpecialDay");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_11_SpecialDay");
                #region Attribute 0x02
                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(2))
                            SetAttributeDecodingResult(2, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                    }
                    else
                    {
                        ///Array Of SpecialDayProfile Structure
                        if (current_char != (byte)DataTypes._A01_array)
                        {
                            SetAttributeDecodingResult(2, DecodingResult.DecodingError);

                            throw new DLMSDecodingException(String.Format("Invalid data Type,array data type is expected (Error Code:{0})",
                                (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_11_SpecialDayTable");
                        }
                        int SpecialDaysCount = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                        int SpDayCount = 1;
                        byte[] ReadValues = null;
                        this.specialDayProfiles.Clear();
                        while (SpDayCount <= SpecialDaysCount)
                        {
                            #region Decode Special Day Profile Entry
                            try
                            {
                                // <DataType TAG>Structure ,Three Elements,<DataType TAG> INDEX
                                to_Compare_Array = new byte[] { (byte)DataTypes._A02_structure, 0x03, (byte)DataTypes._A12_long_unsigned };
                                ReadValues = new byte[3];
                                Array.Copy(Data, array_traverse, ReadValues, 0, ReadValues.Length);
                                array_traverse += 3;
                                if (!to_Compare_Array.SequenceEqual<byte>(ReadValues))
                                {
                                    SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                                    throw new DLMSDecodingException(String.Format("Invalid Special Day Profile structure (Error Code:{0})",
                                        (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_11_SpecialDayTable");
                                }
                                array_traverse--;
                                ///Decoding Special Day Index
                                ushort SpecialDayIndex = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
                                if (!((SpecialDayIndex > 0) && (SpecialDayIndex < ushort.MaxValue)))
                                {
                                    SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                                    throw new DLMSDecodingException(String.Format("Invalid Special Day Profile Index {0}",
                                                                                SpecialDayIndex), "Decode_Data_Class_11_SpecialDayTable");
                                }
                                ///Decode SpecialDay Date 
                                StDateTime SpecialDayDate = new StDateTime();
                                SpecialDayDate.DecodeRawBytes(Data, ref array_traverse);
                                //ReadValues = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                                //DateTime SpecialDayDate = BasicEncodeDecode.Decode_Date(ReadValues);

                                ///Decode Day Profile ID
                                byte DayProfileId = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
                                StSpecialDayProfile SPDayProfile = new StSpecialDayProfile(SpecialDayIndex, SpecialDayDate, DayProfileId);
                                this.specialDayProfiles.Add(SPDayProfile);
                                SpDayCount++;
                            }
                            catch (Exception ex)
                            {
                                if (ex is DLMSDecodingException)
                                {
                                    throw ex;
                                }
                                throw new DLMSDecodingException(String.Format("Error occurred while decoding SpecialDayProfile Entity (Error Code:{0})",
                                        (int)DLMSErrors.ErrorDecoding_Type), "Decode_Data_Class_11_SpecialDayTable", ex);
                            }

                            #endregion
                        }
                        if (SpecialDaysCount != specialDayProfiles.Count)
                            throw new DLMSDecodingException(String.Format("Invalid Special Day Profile Table Count {0} (Error Code:{1})",
                                                                            SpDayCount, (int)DLMSErrors.ErrorDecoding_Type),
                                                                            "Decode_Data_Class_11_SpecialDayTable");
                    }
                    SetAttributeDecodingResult(2, DecodingResult.Ready);
                }
                #endregion
                // make data array ready for upcoming objects
                ///DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding data", "Decode_Data_Class_11_SpecialDayTable", ex);
            }
        }
        /// <summary>
        /// Encode the send request 
        /// </summary>
        /// <returns>byte[]</returns>
        public override byte[] Encode_Data()
        {
            try
            {
                EncodedRaw = new List<byte>(0x0A);
                //------------------------------------------------------
                EncoderAttribute_0();
                EncoderLogicalName();
                //------------------------------------------------------
                #region Attribute 0x02
                if (EncodingAttribute == 0x02 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x02);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Class_11_SpecialDayProfile");
                    }
                    ///Encode Here Data
                    else if (EncodingAttribute == 0x02)
                    {
                        byte[] tarray = null;
                        int entityIndex = 0;
                        int specialDayCount = specialDayProfiles.Count;
                        ///Encode <DataType Array TAG> Array ELEment Count
                        EncodedRaw.Add((byte)DataTypes._A01_array);
                        BasicEncodeDecode.Encode_Length(ref tarray, (ushort)specialDayCount);
                        EncodedRaw.AddRange(tarray);
                        while (entityIndex < specialDayCount)
                        {
                            StSpecialDayProfile spEncode = specialDayProfiles[entityIndex++];
                            ///<DataType TAG>Structure ,Three Elements
                            tarray = new byte[] { (byte)DataTypes._A02_structure, 0x03 };
                            EncodedRaw.AddRange(tarray);
                            ///Encode Special Day Index
                            tarray = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, spEncode.Index);
                            EncodedRaw.AddRange(tarray);
                            ///Encode Special StartDate
                            //BasicEncodeDecode.Encode_Date(spEncode.Date, ref tarray);
                            //tarray = BasicEncodeDecode.Encode_OctetString((Array)tarray.Clone(), DataTypes._A09_octet_string);
                            tarray = spEncode.Date.EncodeRawBytes(StDateTime.DateTimeType.Date);
                            EncodedRaw.AddRange(tarray);
                            ///Encode Day ProfileId
                            tarray = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, spEncode.DayProfileId);
                            EncodedRaw.AddRange(tarray);
                        }
                    }
                }
                ///------------------------------------------------------ 
                #endregion
                byte[] dtBuf = EncodedRaw.ToArray<byte>();
                EncodedRaw = null;
                return dtBuf;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSException(String.Format("{0}_{1} (Error Code:{2})",
                        "Error occurred while encoding data", "Encode_Data_Class_11_SpecialDayProfile", (int)DLMSErrors.ErrorEncoding_Type), ex);
            }
        }

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_11 cloned = new Class_11(this);
            if (cloned.specialDayProfiles != null && cloned.specialDayProfiles.Count > 0)
                cloned.specialDayProfiles.Clear();
            return cloned;
        }

        /// <summary>
        /// Returns the String representation of the Class_11 object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

    }
}
