using DLMS;
using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS
{
    public class Class_71
    {
        //#region Defined Const

        //public const byte NO_STATION = 0x00;

        //public const byte CALLINGDevice = 0x7E;
        //public const byte Broadcast = 0x7F;
        //public const byte AddressSpaceLow = 0x10;                 // 0x10...0x7D
        //public const byte AddressSpaceHigh = 0x7D;

        //public const byte NO_STATION_LongAddress = 0x0000;
        //public const ushort CALLINGDevice_LongAddress = 0x3FFE;
        //public const ushort Broadcast_LongAddress = 0x3FFF;
        //public const ushort AddressSpaceLow_LongAddress = 0x0010; // 0x0010...0x3FFD
        //public const ushort AddressSpaceHigh_LongAddress = 0x3FFD;

        //public const ushort NoInActivityTimeOut = 00;
        //public const ushort DefaultInActivityTimeOut = 120;

        //#endregion

        //#region Data_Members

        //internal MonitoredValue _MonitoredValue = null;
        //internal MonitoredValue _Threshhold_Active = null;
        //public byte WindowSizeTransmit = 1;
        //public byte WindowSizeReceive = 1;
        //public ushort MaxInfoFieldLengthTransmit = 128;
        //public ushort MaxInfoFieldLengthReceive = 128;
        //public ushort InterOctetTimeOut = 25;
        //public ushort InActivityTimeOut = DefaultInActivityTimeOut;
        //public ushort DeviceAddress = 0x0010;

        //#endregion

        //#region Constructor

        //public Class_71(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
        //    : base(23, 9, 0, Index, Obis_Code, No_of_Associations)
        //{
        //}

        //public Class_71(byte[] Obis_Code, byte Attribute_recieved)
        //    : base(23, 9, 0, Obis_Code)
        //{
        //    DecodingAttribute = Attribute_recieved;
        //}

        //public Class_71(StOBISCode OBISCodeStruct)
        //    : base(OBISCodeStruct, 9, 0)
        //{

        //}

        ///// <summary>
        ///// Copy Constructor Class_23
        ///// </summary>
        ///// <param name="obj"></param>
        //public Class_71(Class_71 obj)
        //    : base(obj)
        //{

        //    this.WindowSizeTransmit = obj.WindowSizeTransmit;
        //    this.WindowSizeReceive = obj.WindowSizeReceive;
        //    this.MaxInfoFieldLengthTransmit = obj.MaxInfoFieldLengthTransmit;
        //    this.MaxInfoFieldLengthReceive = obj.MaxInfoFieldLengthReceive;
        //    this.InterOctetTimeOut = obj.InterOctetTimeOut;
        //    this.InActivityTimeOut = obj.InActivityTimeOut;
        //    this.DeviceAddress = obj.DeviceAddress;
        //}

        //#endregion

        //#region Decoder / Encoder

        //// GetRequest Decoder
        //public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        //{
        //    //---------------------------------------------
        //    byte[] Obis_code_recieved = null;
        //    byte current_char;

        //    if (DecodingAttribute == 0x00)
        //    {
        //        for (int index = 0; index < AccessResults.Length; index++)
        //            AccessResults[index] = DecodingResult.DataNotPresent;
        //    }
        //    else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
        //    {
        //        SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
        //    }

        //    //------------------------------------------------------
        //    try
        //    {
        //        DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_23_Object");
        //        DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_23_Object");
        //        //------------------------------------------------------
        //        #region Attribute 0x02

        //        if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
        //        {
        //            current_char = Data[array_traverse++];
        //            if (current_char != (byte)DataTypes._A16_enum)
        //            {
        //                SetAttributeDecodingResult(2, DecodingResult.DecodingError);
        //                throw new DLMSDecodingException(String.Format("{0} invalid identifier of value", this.OBISIndex), "Decode_Data_Class_23");
        //            }
        //            Comm_Speed = (CommSpeed)Data[array_traverse++];
        //            SetAttributeDecodingResult(2, DecodingResult.Ready);
        //        }

        //        #endregion
        //        //------------------------------------------------------
        //        #region Attribute 0x03

        //        if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
        //        {
        //            try
        //            {
        //                current_char = Data[array_traverse++];
        //                if (current_char != (byte)DataTypes._A11_unsigned)
        //                {
        //                    SetAttributeDecodingResult(0x03, DecodingResult.DecodingError);
        //                    throw new DLMSDecodingException(String.Format("{0} invalid identifier of value", this.OBISIndex), "Decode_Data_Class_23");
        //                }
        //                WindowSizeTransmit = Data[array_traverse++];
        //                SetAttributeDecodingResult(0x03, DecodingResult.Ready);
        //            }
        //            catch (Exception ex)
        //            {

        //                SetAttributeDecodingResult(03, DecodingResult.DecodingError);
        //                throw new DLMSDecodingException(String.Format("{0} Error Decoding Attribute 0x03", this.OBISIndex),
        //                    "Decode_Data_Class_23_Object", ex);
        //            }
        //        }

        //        #endregion
        //        //------------------------------------------------------
        //        #region Attribute 0x04

        //        if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
        //        {
        //            try
        //            {
        //                current_char = Data[array_traverse++];
        //                if (current_char != (byte)DataTypes._A11_unsigned)
        //                {
        //                    SetAttributeDecodingResult(0x04, DecodingResult.DecodingError);
        //                    throw new DLMSDecodingException(String.Format("{0} invalid identifier of value", this.OBISIndex), "Decode_Data_Class_23");
        //                }

        //                WindowSizeReceive = Data[array_traverse++];
        //                SetAttributeDecodingResult(0x04, DecodingResult.Ready);
        //            }
        //            catch (Exception ex)
        //            {
        //                SetAttributeDecodingResult(04, DecodingResult.DecodingError);
        //                throw new DLMSDecodingException(String.Format("{0} Error Decoding Attribute 0x04", this.OBISIndex), "Decode_Data_Class_23_Object", ex);
        //            }
        //        }

        //        #endregion
        //        //------------------------------------------------------
        //        #region Attribute 0x05

        //        if (DecodingAttribute == 0x05 || DecodingAttribute == 0x00)
        //        {
        //            try
        //            {
        //                current_char = Data[array_traverse++];
        //                if (current_char != (byte)DataTypes._A12_long_unsigned)
        //                {
        //                    SetAttributeDecodingResult(0x05, DecodingResult.DecodingError);
        //                    throw new DLMSDecodingException(String.Format("{0} invalid identifier of value", this.OBISIndex), "Decode_Data_Class_23");
        //                }
        //                array_traverse--;
        //                MaxInfoFieldLengthTransmit = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
        //                SetAttributeDecodingResult(0x05, DecodingResult.Ready);
        //            }
        //            catch (Exception ex)
        //            {

        //                SetAttributeDecodingResult(05, DecodingResult.DecodingError);
        //                throw new DLMSDecodingException(String.Format("{0} Error Decoding Attribute 0x05", this.OBISIndex),
        //                    "Decode_Data_Class_23_Object", ex);
        //            }
        //        }

        //        #endregion
        //        //------------------------------------------------------
        //        #region Attribute 0x06

        //        if (DecodingAttribute == 0x06 || DecodingAttribute == 0x00)
        //        {
        //            try
        //            {
        //                current_char = Data[array_traverse++];
        //                if (current_char != (byte)DataTypes._A12_long_unsigned)
        //                {
        //                    SetAttributeDecodingResult(0x06, DecodingResult.DecodingError);
        //                    throw new DLMSDecodingException(String.Format("{0} invalid identifier of value", this.OBISIndex), "Decode_Data_Class_23");
        //                }
        //                array_traverse--;
        //                MaxInfoFieldLengthReceive = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
        //                SetAttributeDecodingResult(0x06, DecodingResult.Ready);
        //            }
        //            catch (Exception ex)
        //            {

        //                SetAttributeDecodingResult(06, DecodingResult.DecodingError);
        //                throw new DLMSDecodingException(String.Format("{0} Error Decoding Attribute 0x06", this.OBISIndex),
        //                    "Decode_Data_Class_23_Object", ex);
        //            }
        //        }

        //        #endregion
        //        //------------------------------------------------------
        //        #region Attribute 0x07

        //        if (DecodingAttribute == 0x07 || DecodingAttribute == 0x00)
        //        {
        //            try
        //            {
        //                current_char = Data[array_traverse++];
        //                if (current_char != (byte)DataTypes._A12_long_unsigned)
        //                {
        //                    SetAttributeDecodingResult(0x07, DecodingResult.DecodingError);
        //                    throw new DLMSDecodingException(String.Format("{0} invalid identifier of value", this.OBISIndex), "Decode_Data_Class_23");
        //                }
        //                array_traverse--;
        //                InterOctetTimeOut = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
        //                SetAttributeDecodingResult(0x07, DecodingResult.Ready); SetAttributeDecodingResult(0x03, DecodingResult.Ready);
        //            }
        //            catch (Exception ex)
        //            {

        //                SetAttributeDecodingResult(07, DecodingResult.DecodingError);
        //                throw new DLMSDecodingException(String.Format("{0} Error Decoding Attribute 0x07", this.OBISIndex),
        //                    "Decode_Data_Class_23_Object", ex);
        //            }
        //        }

        //        #endregion
        //        //------------------------------------------------------
        //        #region Attribute 0x08

        //        if (DecodingAttribute == 0x08 || DecodingAttribute == 0x00)
        //        {
        //            try
        //            {
        //                current_char = Data[array_traverse++];
        //                if (current_char != (byte)DataTypes._A12_long_unsigned)
        //                {
        //                    SetAttributeDecodingResult(0x08, DecodingResult.DecodingError);
        //                    throw new DLMSDecodingException(String.Format("{0} invalid identifier of value", this.OBISIndex), "Decode_Data_Class_23");
        //                }
        //                array_traverse--;
        //                InActivityTimeOut = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
        //                SetAttributeDecodingResult(0x08, DecodingResult.Ready);
        //            }
        //            catch (Exception ex)
        //            {

        //                SetAttributeDecodingResult(08, DecodingResult.DecodingError);
        //                throw new DLMSDecodingException(String.Format("{0} Error Decoding Attribute 0x08", this.OBISIndex),
        //                    "Decode_Data_Class_23_Object", ex);
        //            }
        //        }

        //        #endregion
        //        //------------------------------------------------------
        //        #region Attribute 0x09

        //        if (DecodingAttribute == 0x09 || DecodingAttribute == 0x00)
        //        {
        //            try
        //            {
        //                current_char = Data[array_traverse++];
        //                if (current_char != (byte)DataTypes._A12_long_unsigned)
        //                {
        //                    SetAttributeDecodingResult(0x09, DecodingResult.DecodingError);
        //                    throw new DLMSDecodingException(String.Format("{0} invalid identifier of value", this.OBISIndex), "Decode_Data_Class_23");
        //                }
        //                array_traverse--;
        //                DeviceAddress = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
        //                SetAttributeDecodingResult(0x09, DecodingResult.Ready);
        //            }
        //            catch (Exception ex)
        //            {

        //                SetAttributeDecodingResult(09, DecodingResult.DecodingError);
        //                throw new DLMSDecodingException(String.Format("{0} Error Decoding Attribute 0x09", this.OBISIndex),
        //                    "Decode_Data_Class_23_Object", ex);
        //            }
        //        }

        //        #endregion
        //        //------------------------------------------------------
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is DLMSDecodingException)
        //        {
        //            throw ex;
        //        }
        //        else
        //        {
        //            throw new DLMSException(String.Format("{0}_{1}_{2}", "Error occurred while decoding data ", OBISIndex, "Decode_Data_Class_23_Object"), ex);
        //        }
        //    }
        //}

        //// Set Request Encoder
        //public override byte[] Encode_Data()
        //{
        //    try
        //    {
        //        EncodedRaw = new List<byte>(0x0A);
        //        //------------------------------------------------------
        //        EncoderAttribute_0();
        //        EncoderLogicalName();
        //        //------------------------------------------------------
        //        #region Attribute 0x02

        //        if (EncodingAttribute == 0x02 || EncodingAttribute == 0x00)
        //        {
        //            EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A16_enum, Comm_Speed));
        //        }

        //        #endregion

        //        #region Attribute 0x03

        //        if (EncodingAttribute == 0x03 || EncodingAttribute == 0x00)
        //        {
        //            EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, WindowSizeTransmit));
        //        }

        //        #endregion

        //        #region Attribute 0x04

        //        if (EncodingAttribute == 0x04 || EncodingAttribute == 0x00)
        //        {
        //            EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, WindowSizeReceive));
        //        }

        //        #endregion

        //        #region Attribute 0x05

        //        if (EncodingAttribute == 0x05 || EncodingAttribute == 0x00)
        //        {
        //            EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, MaxInfoFieldLengthTransmit));
        //        }

        //        #endregion

        //        #region Attribute 0x06

        //        if (EncodingAttribute == 0x06 || EncodingAttribute == 0x00)
        //        {
        //            EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, MaxInfoFieldLengthReceive));
        //        }

        //        #endregion

        //        #region Attribute 0x07

        //        if (EncodingAttribute == 0x07 || EncodingAttribute == 0x00)
        //        {
        //            EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, InterOctetTimeOut));
        //        }

        //        #endregion

        //        #region Attribute 0x08

        //        if (EncodingAttribute == 0x08 || EncodingAttribute == 0x00)
        //        {
        //            EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, InActivityTimeOut));
        //        }

        //        #endregion

        //        #region Attribute 0x09

        //        if (EncodingAttribute == 0x09 || EncodingAttribute == 0x00)
        //        {
        //            EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, DeviceAddress));
        //        }

        //        #endregion

        //        byte[] dtBuf = EncodedRaw.ToArray<byte>();
        //        EncodedRaw = null;
        //        return dtBuf;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is DLMSEncodingException)
        //        {
        //            throw ex;
        //        }
        //        else
        //            throw new DLMSException(String.Format("{0}_{1}", "Error occurred while encoding data", "Encode_Data_Class_23"), ex);
        //    }
        //}

        //#endregion

        //#region Member Methods

        //public override object Clone()
        //{
        //    Class_71 cloned = new Class_71(this);
        //    return cloned;
        //}

        //#endregion
    }
}
