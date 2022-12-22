using System;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// Extended register (class_id: 4, version: 0) allows modeling a process value with its associated scaler, unit, status and capture time information.
    /// Extended register objects know the nature of the process value. It is described by the attribute Index <see cref="StOBISCode"/>(logical_name)
    /// </summary>
    public class Class_4 : Base_Class
    {
        #region Data Members
        
        /// <summary>
        /// Value COSEM Interface Class 4 Attribute 0x02;Value Property To hold double type of data (value as double)<see cref="DLMS.Comm.DataTypes"/> 
        /// </summary>
        public double Value;
        /// <summary>
        /// <see cref="DLMS.Comm.Unit_Scaler"/> COSEM Interface Class 4 Attribute 0x03;Unit is an Enumeration which is the exponent (to the base of 10) of the multiplication factor.
        /// </summary>
        public units Unit;
        /// <summary>
        /// <see cref="DLMS.Comm.Unit_Scaler"/> COSEM Interface Class 4 Attribute 0x03;Scaler value that would later composite with Unit
        /// </summary>
        public sbyte scaler;
        /// <summary>
        /// <see cref="DLMS.Comm.StDateTime"/> COSEM Interface Class 4 Attribute 0x04;Provides an Extended register specific date and time
        /// information showing when the value of the attribute value has been captured.
        /// </summary>
        public StDateTime Date_Time_Stamp;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_4(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(4, 5, 1, Index, Obis_Code, No_of_Associations)
        {
            // Create array
            Date_Time_Stamp = new StDateTime();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="Attribute_recieved">no of attribute associated with current object</param>
        public Class_4(byte[] Obis_Code, byte Attribute_recieved)
            : base(4, 5, 1, Obis_Code)
        {
            DecodingAttribute = Attribute_recieved;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCode">OBIS code for a specific Object</param>
        public Class_4(StOBISCode OBISCode)
            : base(OBISCode, 5, 1)
        {
            Date_Time_Stamp = new StDateTime();
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="obj">Class_4 Object</param>
        public Class_4(Class_4 obj)
            : base(obj)
        {
            Value = obj.Value;
            Unit = obj.Unit;
            if (obj.Date_Time_Stamp != null)
                Date_Time_Stamp = new StDateTime(obj.Date_Time_Stamp);
            else
                Date_Time_Stamp = new StDateTime();
        }

        #endregion

        #region Decoders/Encoders

        /// <summary>
        /// Decode Data of this Class which is received in response of get data Request
        /// </summary>
        /// <param name="Data">Received data from Remote site</param>
        /// <param name="array_traverse">Off-Set</param>
        /// <param name="length">Length to decode</param>
        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            byte[] Obis_code_recieved = null;
            byte current_char;
            
            try
            {
                if (DecodingAttribute == 0x00)
                {
                    for (int index = 0; index < AccessResults.Length; index++)
                        AccessResults[index] = DecodingResult.DataNotPresent;
                }
                else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
                {
                    SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
                }
                //------------------------------------------------------
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_4_Object");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_4_Object");
                //------------------------------------------------------
                #region Attribute 0x02
                
                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        ValueType _Value = BasicEncodeDecode.Intelligent_Data_Decoder(
                           ref Data, ref array_traverse, Data.Length);
                        Value = Convert.ToDouble(_Value);
                        SetAttributeDecodingResult(02, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        if (!IsAttribReadable(02))
                            SetAttributeDecodingResult(02, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(02, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_4_Object", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x03
                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        current_char = Data[array_traverse++];
                        if (current_char != (byte)DataTypes._A02_structure || 
                            Data[array_traverse++] != 0x02)
                        {
                            // Generate Error
                            SetAttributeDecodingResult(03, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of scaler_unit structure  (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_4_Object");
                        }
                        ValueType vl = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data,
                        ref array_traverse, Data.Length);
                        byte _scaler = Convert.ToByte(vl);
                        unchecked
                        {
                            this.scaler = (sbyte)_scaler;
                        }
                        Unit = (units)Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data,
                        ref array_traverse, Data.Length));
                        SetAttributeDecodingResult(0x03, DecodingResult.Ready);
                    }
                    catch (DLMSDecodingException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        if (!IsAttribReadable(03))
                            SetAttributeDecodingResult(03, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(03, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of scaler in scaler_unit (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_4_Object", ex);
                        }
                    }
                    //Perform Value Adjustment According Scaler_Unit Structure
                    Value = Convert.ToDouble(BasicEncodeDecode.ValueUnitScalerAdjustment(Value, scaler));

                }
                #endregion
                //------------------------------------------------------
                #region Attribute 0x04
                
                if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(04))
                            SetAttributeDecodingResult(04, DecodingResult.NoAccess);
                        else
                            SetAttributeDecodingResult(04, DecodingResult.DecodingError);
                    }
                    else
                    {
                        BasicEncodeDecode.Skip_Elements(Data, ref array_traverse);
                        // SetAttributeDecodingResult(4, DecodingResult.Ready);
                    }
                }
                #endregion
                //------------------------------------------------------
                #region Attribute 0x05

                if (DecodingAttribute == 0x05 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        current_char = Data[array_traverse++];
                        if (current_char == (byte)DataTypes._A00_Null)
                        {
                            // Check Access Rights
                            if (!IsAttribReadable(0x05))
                                SetAttributeDecodingResult(0x05, DecodingResult.NoAccess);
                            else
                                SetAttributeDecodingResult(0x05, DecodingResult.DecodingError);
                        }
                        else
                        {
                            this.Date_Time_Stamp = new StDateTime();
                            array_traverse--;
                            this.Date_Time_Stamp.DecodeRawBytes(Data, ref array_traverse);
                            SetAttributeDecodingResult(5, DecodingResult.Ready);
                        }


                    }
                    catch (Exception ex)
                    {
                        //Check access rights
                        if (!IsAttribReadable(0x05))
                            SetAttributeDecodingResult(05, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(05, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} element is not 09 (octet string)_decoding DateTime_Stamp (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_4_Object");
                        }
                    }
                }

                #endregion
                // make data array ready for upcoming objects
                // DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                {
                    throw new DLMSDecodingException(String.Format("{0}_{1} element is not 09 (octet string)_decoding DateTime_Stamp (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_4_Object");
                }
            }
        }
        /// <summary>
        /// Set the Request Encoder
        /// </summary>
        /// <returns>byte[]</returns>
        public override byte[] Encode_Data()
        {
            byte[] data_array = new byte[1];
            return data_array;
        }

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_4 cloned = new Class_4(this);
            return cloned;
        }

        /// <summary>
        /// Returns the String representation of the Class_4 object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            string baseStr = base.ToString();
            string time_str = "NIL";
            
            if (Date_Time_Stamp != null && GetAttributeDecodingResult(5) == DecodingResult.Ready)
                time_str = Date_Time_Stamp.ToString();

            StringBuilder strVal = new StringBuilder();
            strVal.AppendFormat(",Requested Value:{0:f3}:{1}", Value, GetAttributeDecodingResult(2));
            strVal.AppendFormat(",Unit:{0}:{1}", Unit, GetAttributeDecodingResult(3));
            // Missing Attributes 04 Status
            strVal.AppendFormat(",Capture_time:{0}:{1}", time_str, GetAttributeDecodingResult(5));

            return baseStr + strVal.ToString();
        }

        #endregion
    }

}
