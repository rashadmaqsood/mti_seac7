using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Globalization;

namespace DLMS.Comm
{
    /// <summary>
    /// OBIS defines identification codes for commonly used data items in metering equipment.
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(StOBISCode))]
    public struct StOBISCode
    {
        #region DataMembers
        /// <summary>
        /// Regular Expression To validate OBIS Code In Hex Number Format
        /// </summary>
        public static readonly string OBISValidatorHex = @"^(?<OBISCode>(?<CLS>(?<ClassId>(?<Hex>[a-fA-F0-9]){4})[;,.'])?(?<FeildA>(?<Hex>[a-fA-F0-9]){2})\.(?<FeildB>(?<Hex>[a-fA-F0-9]){2})\.(?<FeildC>(?<Hex>[a-fA-F0-9]){2})\.(?<FeildD>(?<Hex>[a-fA-F0-9]){2})\.(?<FeildE>(?<Hex>[a-fA-F0-9]){2})\.(?<FeildF>(?<Hex>[a-fA-F0-9]){2}))$";
        /// <summary>
        /// Regular Expression To validate OBIS Code In Decimal Number Format
        /// </summary>
        public static readonly string OBISValidator = @"(?<OBISCOde>^(?<CLS>(?<ClassId>\d{1,5})[:;',])?(?<FieldA>\d{1,3})\.(?<FieldB>\d{1,3})\.(?<FieldC>\d{1,3})\.(?<FieldD>\d{1,3})\.(?<FieldE>\d{1,3})\.(?<FieldF>\d{1,3})$)";

        /// <summary>
        /// Regular Expression To find OBIS Code Patterns In Hex Number Format
        /// </summary>
        public static readonly string OBISPatternValidatorHex = @"^(?<OBISCode>(?<CLS>(?<ClassId>(?<Hex>[a-fA-F0-9]){4})[;,.'])?(?<AA>((?<FeildA>(?<Hex>[a-fA-F0-9]){2})|([\x21-\x2Dg-zG-Z]{1,2}))\.)(?<BB>((?<FeildB>(?<Hex>[a-fA-F0-9]){2})|([\x21-\x2Dg-zG-Z]{1,2}))\.)(?<CC>((?<FeildC>(?<Hex>[a-fA-F0-9]){2})|([\x21-\x2Dg-zG-Z]{1,2}))\.)(?<DD>((?<FeildD>(?<Hex>[a-fA-F0-9]){2})|([\x21-\x2Dg-zG-Z]{1,2}))\.)(?<EE>((?<FeildE>(?<Hex>[a-fA-F0-9]){2})|([\x21-\x2Dg-zG-Z]{1,2}))\.)(?<FF>((?<FeildF>(?<Hex>[a-fA-F0-9]){2}))))";

        /// <summary>
        /// Regular Expression To validate OBIS Code Patterns In Decimal Number Format
        /// </summary>
        public static readonly string OBISPatternValidator = @"(?<OBISCOde>^(?<CLS>(?<ClassId>\d{1,5})[:;',])?(?<AA>((?<FieldA>\d{1,3})|[\x21-\x2Da-zA-Z]{1,3})\.)(?<BB>((?<FieldB>\d{1,3})|[\x21-\x2Da-zA-Z]{1,3})\.)(?<CC>((?<FieldC>\d{1,3})|[\x21-\x2Da-zA-Z]{1,3})\.)(?<DD>((?<FieldD>\d{1,3})|[\x21-\x2Da-zA-Z]{1,3})\.)(?<EE>((?<FieldE>\d{1,3})|[\x21-\x2Da-zA-Z]{1,3})\.)(?<FF>((?<FieldF>\d{1,3})|[\x21-\x2Da-zA-Z]{1,3}))$)";

        private Get_Index OBIS_Index;
        /// <summary>
        /// Specify the format to parse an OBIS code into given format
        /// </summary>
        public enum FormatSpecifier : byte
        {
            CompleteHexMode,
            CompleteDecimalMode,
            ShortHexMode,
            ShortDecimalMode
        }
        #endregion
        #region Properties
        /// <summary>
        /// Get/Set the <see cref="Get_Index"/> to identify the data quantity which object is holding currently 
        /// </summary>
        [XmlIgnore()]
        public Get_Index OBISIndex
        {
            get { return OBIS_Index; }
            set { OBIS_Index = value; }
        }
        /// <summary>
        /// Validate whether OBISIndex is holding valid index or not
        /// </summary>
        public bool IsValidate
        {
            get
            {
                try
                {
                    string obisName = Enum.GetName(typeof(Get_Index), OBISIndex);

                    if (String.IsNullOrEmpty(obisName) || obisName.Contains("Dummy"))
                    {
                        if (ClassId > 0 &&
                            (0x0000FFFFFFFFFFFF & OBIS_Value) > 0)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Get/Set the OBIS_Index value in long int
        /// </summary>
        [XmlElement("OBISCodeRaw", Type = typeof(ulong))]
        public ulong OBIS_Value
        {
            get
            {
                return Convert.ToUInt64(OBISIndex);
            }
            set
            {
                this.OBISIndex = (Get_Index)value;
            }
        }

        [XmlIgnore()]
        public ObjectType ObjectType
        {
            get
            {
                return (ObjectType)ClassId;
            }
            set
            {
                ClassId = Convert.ToUInt16(value);
            }
        }

        /// <summary>
        /// Get/Set the Class id as per given in DLMS/COSEM specification
        /// </summary>
        [XmlIgnore()]
        public ushort ClassId
        {
            get
            {
                return (ushort)((0xFFFF000000000000 & OBIS_Value) >> 48);
            }
            set
            {
                ulong tVal = OBIS_Value;
                tVal = ((0x0000FFFFFFFFFFFF & OBIS_Value));
                tVal = (((ulong)value << 48) | tVal);
                this.OBIS_Index = (Get_Index)tVal;
            }
        }
        /// <summary>
        /// GET OBIS code in bytes
        /// </summary>
        [XmlIgnore()]
        public byte[] OBISCode
        {
            get
            {
                try
                {
                    byte[] OBISCode = new byte[] { 0, 0, 0, 0, 0, 0 };
                    OBISCode[0] = OBISCode_Feild_A;
                    OBISCode[1] = OBISCode_Feild_B;
                    OBISCode[2] = OBISCode_Feild_C;
                    OBISCode[3] = OBISCode_Feild_D;
                    OBISCode[4] = OBISCode_Feild_E;
                    OBISCode[5] = OBISCode_Feild_F;
                    return OBISCode;
                }
                catch (Exception ex)
                {

                    throw new Exception(String.Format("unable to determine OBIS Code for {0}", this.OBISIndex), ex);
                }
            }
        }

        #region Individual_OBIS_Feilds
        /// <summary>
        /// Get the OBIS code Value of field A of an OBIS code further read OBIS code Structure defined by the DLMS/COSEM
        /// </summary>
        public byte OBISCode_Feild_A
        {
            get
            {
                return (byte)((0x0000FF0000000000 & OBIS_Value) >> 40);
            }
        }
        /// <summary>
        /// Set the OBIS code Value of field A of an OBIS code further read OBIS code Structure defined by the DLMS/COSEM
        /// </summary>
        /// <param name="value">value to set</param>
        /// <returns></returns>
        internal StOBISCode Set_OBISCode_Feild_A(byte value)
        {
            try
            {
                ulong OBISValueT = OBIS_Value;
                OBISValueT = (ulong)((0xFFFF00FFFFFFFFFF & OBISValueT) | ((ulong)value << 40));
                StOBISCode ToRet = (Get_Index)OBISValueT;
                return ToRet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to set Feild A for {0}", this.OBISIndex));
            }
        }
        /// <summary>
        /// Get the OBIS code Value of field 'B' of an OBIS code further read OBIS code Structure defined by the DLMS/COSEM
        /// </summary>
        public byte OBISCode_Feild_B
        {
            get
            {
                return (byte)((0x000000FF00000000 & OBIS_Value) >> 32);
            }
        }
        /// <summary>
        /// Set the OBIS code Value of field 'B' of an OBIS code further read OBIS code Structure defined by the DLMS/COSEM
        /// </summary>
        /// <param name="value">Value to set</param>
        /// <returns>New OBIS Code</returns>
        internal StOBISCode Set_OBISCode_Feild_B(byte value)
        {
            try
            {
                ulong OBISValueT = OBIS_Value;
                OBISValueT = (ulong)((0xFFFFFF00FFFFFFFF & OBISValueT) | ((ulong)value << 32));
                StOBISCode ToRet = (Get_Index)OBISValueT;
                return ToRet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to set Feild B for {0}", this.OBISIndex));
            }
        }
        /// <summary>
        /// Get the OBIS code Value of field 'C' of an OBIS code further read OBIS code Structure defined by the DLMS/COSEM
        /// </summary>
        public byte OBISCode_Feild_C
        {
            get
            {
                return (byte)((0x00000000FF000000 & OBIS_Value) >> 24);
            }
        }
        /// <summary>
        /// Set the OBIS code Value of field 'C' of an OBIS code further read OBIS code Structure defined by the DLMS/COSEM
        /// </summary>
        /// <param name="value">Value to set</param>
        /// <returns>New OBIS code</returns>
        internal StOBISCode Set_OBISCode_Feild_C(byte value)
        {
            try
            {
                ulong OBISValueT = OBIS_Value;
                OBISValueT = (ulong)((0xFFFFFFFF00FFFFFF & OBISValueT) | ((ulong)value << 24));
                StOBISCode ToRet = (Get_Index)OBISValueT;
                return ToRet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to set Feild C for {0}", this.OBISIndex));
            }
        }
        /// <summary>
        /// Get the OBIS code Value of field 'D' of an OBIS code further read OBIS code Structure defined by the DLMS/COSEM
        /// </summary>
        public byte OBISCode_Feild_D
        {
            get
            {
                return (byte)((0x0000000000FF0000 & OBIS_Value) >> 16);
            }
        }
        /// <summary>
        /// Get the OBIS code Value of field 'D' of an OBIS code further read OBIS code Structure defined by the DLMS/COSEM
        /// </summary>
        /// <param name="value">Value to set</param>
        /// <returns>New OBIS Code</returns>
        public StOBISCode Set_OBISCode_Feild_D(byte value)
        {
            try
            {
                ulong OBISValueT = OBIS_Value;
                OBISValueT = (ulong)((0xFFFFFFFFFF00FFFF & OBISValueT) | ((ulong)value << 16));
                StOBISCode ToRet = (Get_Index)OBISValueT;
                return ToRet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to set Feild D for {0}", this.OBISIndex));
            }
        }
        /// <summary>
        /// Get the OBIS code Value of field 'E' of an OBIS code further read OBIS code Structure defined by the DLMS/COSEM
        /// </summary>
        public byte OBISCode_Feild_E
        {
            get
            {
                return (byte)((0xFF00 & OBIS_Value) >> 8);
            }
        }
        /// <summary>
        /// Set the OBIS code Value of field 'E' of an OBIS code further read OBIS code Structure defined by the DLMS/COSEM
        /// </summary>
        /// <param name="value">Value to set</param>
        /// <returns>New OBIS code</returns>
        public StOBISCode Set_OBISCode_Feild_E(byte value)
        {
            try
            {
                ulong OBISValueT = OBIS_Value;
                OBISValueT = (ulong)((0xFFFFFFFFFFFF00FF & OBISValueT) | ((ulong)value << 8));
                StOBISCode ToRet = (Get_Index)OBISValueT;
                return ToRet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to set Feild E for {0}", this.OBISIndex));
            }
        }
        /// <summary>
        /// Get the OBIS code Value of field 'F' of an OBIS code further read OBIS code Structure defined by the DLMS/COSEM
        /// </summary>
        public byte OBISCode_Feild_F
        {
            get
            {
                return (byte)((0xFF & OBIS_Value));
            }
        }
        /// <summary>
        /// Get the OBIS code Value of field 'F' of an OBIS code further read OBIS code Structure defined by the DLMS/COSEM
        /// </summary>
        /// <param name="value">Value to Set</param>
        /// <returns>New OBIS code</returns>
        public StOBISCode Set_OBISCode_Feild_F(byte value)
        {
            try
            {
                ulong OBISValueT = this.OBIS_Value;
                OBISValueT = (ulong)((0xFFFFFFFFFFFFFF00 & OBISValueT) | (value));
                StOBISCode ToRet = (Get_Index)OBISValueT;
                return ToRet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to set Feild F for {0}", this.OBISIndex));
            }
        }
        #endregion

        #endregion

        #region Convert Functions
        
        /// <summary>
        /// Convert an OBIS code into bytes
        /// </summary>
        /// <param name="OBISIndex"></param>
        /// <returns></returns>
        public static byte[] ConvertToArray(StOBISCode OBISIndex)
        {
            try
            {
                byte[] rawVal = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                rawVal[0] = OBISIndex.OBISCode_Feild_A;
                rawVal[1] = OBISIndex.OBISCode_Feild_B;
                rawVal[2] = OBISIndex.OBISCode_Feild_C;
                rawVal[3] = OBISIndex.OBISCode_Feild_D;
                rawVal[4] = OBISIndex.OBISCode_Feild_E;
                rawVal[5] = OBISIndex.OBISCode_Feild_F;
                rawVal[6] = (byte)(OBISIndex.ClassId & 0xFF);
                rawVal[7] = (byte)(OBISIndex.ClassId >> 8);
                return rawVal;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("unable to determine OBIS byte values for {0}", OBISIndex.OBISIndex), ex);
            }
        }
        /// <summary>
        /// Convert Bytes into an OBIS code
        /// </summary>
        /// <param name="OBISCodes">bytes to convert</param>
        /// <returns><see cref="StOBISCode"/></returns>
        public static StOBISCode ConvertFrom(byte[] OBISCodes)
        {
            try
            {
                int array_traverse = 0;
                return ConvertFrom(OBISCodes, ref array_traverse, OBISCodes.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get an OBIS code from bytes buffer with specifying offset and length
        /// </summary>
        /// <param name="OBISCodes"></param>
        /// <param name="array_traverse"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static StOBISCode ConvertFrom(byte[] OBISCodes, ref int array_traverse, int length)
        {
            try
            {
                if (OBISCodes == null || (OBISCodes.Length - array_traverse) < length || (length != 6 && length != 8))
                    throw new Exception("Invalid array structure passed");
                ulong t = 0;
                if (length == 8)
                {
                    t = (ulong)(OBISCodes[array_traverse + 06] | (OBISCodes[array_traverse + 07] << 8));
                }
                t = (ulong)((t << 8) | (ulong)OBISCodes[array_traverse + 0]);
                t = (ulong)((t << 8) | (ulong)OBISCodes[array_traverse + 1]);
                t = (ulong)((t << 8) | (ulong)OBISCodes[array_traverse + 2]);
                t = (ulong)((t << 8) | (ulong)OBISCodes[array_traverse + 3]);
                t = (ulong)((t << 8) | (ulong)OBISCodes[array_traverse + 4]);
                t = (ulong)((t << 8) | (ulong)OBISCodes[array_traverse + 5]);
                StOBISCode stOBISCode = (Get_Index)t;
                return stOBISCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Convert an OBIS code into a long int value
        /// </summary>
        /// <param name="stOBISCodeInst"><see cref="StOBISCode"/></param>
        /// <returns>OBIS code</returns>
        public static ulong ConvertTo(StOBISCode stOBISCodeInst)
        {
            return stOBISCodeInst.OBIS_Value;
        }
        /// <summary>
        /// Get <see cref="StOBISCode"/> from Integer value
        /// </summary>
        /// <param name="OBISValue"></param>
        /// <returns></returns>
        public static StOBISCode ConvertFrom(ulong OBISValue)
        {
            try
            {
                StOBISCode OBISCodeInstan = (Get_Index)OBISValue;
                return OBISCodeInstan;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get <see cref="StOBISCode"/> from a String
        /// </summary>
        /// <param name="OBIS_Str"></param>
        /// <returns></returns>
        public static StOBISCode ConvertFrom(string OBIS_Str)
        {
            try
            {
                StOBISCode OBISCode = Get_Index.Dummy;
                Regex OBISHexValidator = new Regex(StOBISCode.OBISValidatorHex, RegexOptions.Compiled);
                Regex OBISValidator = new Regex(StOBISCode.OBISValidator, RegexOptions.Compiled);
                if (OBISHexValidator.IsMatch(OBIS_Str))
                {
                    Match Splits = OBISHexValidator.Match(OBIS_Str);
                    ushort classId = ushort.Parse(Splits.Groups["ClassId"].Value, NumberStyles.HexNumber);
                    byte feildA = byte.Parse(Splits.Groups[5].Value, NumberStyles.HexNumber);
                    byte feildB = byte.Parse(Splits.Groups[6].Value, NumberStyles.HexNumber);
                    byte feildC = byte.Parse(Splits.Groups[7].Value, NumberStyles.HexNumber);
                    byte feildD = byte.Parse(Splits.Groups[8].Value, NumberStyles.HexNumber);
                    byte feildE = byte.Parse(Splits.Groups[9].Value, NumberStyles.HexNumber);
                    byte feildF = byte.Parse(Splits.Groups[10].Value, NumberStyles.HexNumber);

                    // Try To Convert From Array To OBIS Code
                    byte[] rawVal = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                    rawVal[0] = feildA;
                    rawVal[1] = feildB;
                    rawVal[2] = feildC;
                    rawVal[3] = feildD;
                    rawVal[4] = feildE;
                    rawVal[5] = feildF;
                    rawVal[6] = (byte)(classId & 0xFF);
                    rawVal[7] = (byte)(classId >> 8);
                    OBISCode = StOBISCode.ConvertFrom(rawVal);
                }
                else if (OBISValidator.IsMatch(OBIS_Str))
                {
                    Match Splits = OBISValidator.Match(OBIS_Str);
                    ushort classId = ushort.Parse(Splits.Groups["ClassId"].Value);
                    byte feildA = byte.Parse(Splits.Groups["FieldA"].Value);
                    byte feildB = byte.Parse(Splits.Groups["FieldB"].Value);
                    byte feildC = byte.Parse(Splits.Groups["FieldC"].Value);
                    byte feildD = byte.Parse(Splits.Groups["FieldD"].Value);
                    byte feildE = byte.Parse(Splits.Groups["FieldE"].Value);
                    byte feildF = byte.Parse(Splits.Groups["FieldF"].Value);

                    // Try To Convert From Array To OBIS Code
                    byte[] rawVal = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                    rawVal[0] = feildA;
                    rawVal[1] = feildB;
                    rawVal[2] = feildC;
                    rawVal[3] = feildD;
                    rawVal[4] = feildE;
                    rawVal[5] = feildF;
                    rawVal[6] = (byte)(classId & 0xFF);
                    rawVal[7] = (byte)(classId >> 8);
                    OBISCode = StOBISCode.ConvertFrom(rawVal);
                }
                else
                {
                    throw new Exception("Unable to verify OBIS String format");
                }

                return OBISCode;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to validate OBIS String and convert", ex);
            }
        }
        /// <summary>
        /// Get <see cref="StOBISCode"/> from bytes
        /// </summary>
        /// <param name="OBISCodes"></param>
        /// <returns></returns>
        public static StOBISCode FindByOBISCode(byte[] OBISCodes)
        {
            StOBISCode code = Get_Index.Dummy;
            try
            {
                code = StOBISCode.ConvertFrom(OBISCodes);
                ulong[] OBISCodesValues = (ulong[])Enum.GetValues(typeof(Get_Index));
                foreach (var OBISValue in OBISCodesValues)
                {
                    StOBISCode OBIST = (Get_Index)OBISValue;
                    if ((OBIST.OBIS_Value & 0x0000FFFFFFFFFFFF) == code.OBIS_Value)
                    {

                        return OBIST;
                    }
                }
                throw new Exception("Not found");
            }
            catch (Exception ex)
            {
                return code;
            }
        }

        public static StOBISCode FindByOBISCode(List<StOBISCode> OBISCodesLst, byte[] OBISCodes)
        {
            StOBISCode code = Get_Index.Dummy;
            try
            {
                code = StOBISCode.ConvertFrom(OBISCodes);

                // ulong[] OBISCodesValues = (ulong[])Enum.GetValues(typeof(Get_Index));

                foreach (StOBISCode OBISValue in OBISCodesLst)
                {
                    // StOBISCode OBIST = (Get_Index)OBISValue;
                    if ((OBISValue.OBIS_Value & 0x0000FFFFFFFFFFFF) == code.OBIS_Value)
                    {
                        return OBISValue;
                    }
                }

                throw new Exception(string.Format("{0} Not found", code.ToString(FormatSpecifier.ShortDecimalMode)));
            }
            catch (Exception ex)
            {
                code = Get_Index.Dummy;
                return code;
            }
        }

        #endregion
        
        /// <summary>
        /// Implicit Operator to convert <see cref="Get_Index"/> to <see cref="StOBISCode"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator StOBISCode(Get_Index value)
        {
            return new StOBISCode() { OBIS_Index = value };
        }

        public static bool operator ==(StOBISCode a, StOBISCode b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(StOBISCode a, StOBISCode b)
        {
            return !a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            try
            {
                return this.OBIS_Value == ((StOBISCode)obj).OBIS_Value;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        #region To_StringMethods
        public override string ToString()
        {
            try
            {
                return String.Format("{0}.{1}.{2}.{3}.{4}.{5}",
                    OBISCode_Feild_A,
                    OBISCode_Feild_B,
                    OBISCode_Feild_C,
                    OBISCode_Feild_D,
                    OBISCode_Feild_E,
                    OBISCode_Feild_F);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ToString(FormatSpecifier formatSpecifier)
        {
            try
            {
                string OBIS = null;
                switch (formatSpecifier)
                {
                    case FormatSpecifier.CompleteHexMode:
                        OBIS = String.Format("{0:X2}:{1:X2}.{2:X2}.{3:X2}.{4:X2}.{5:X2}.{6:X2}",
                    this.ClassId,
                    OBISCode_Feild_A,
                    OBISCode_Feild_B,
                    OBISCode_Feild_C,
                    OBISCode_Feild_D,
                    OBISCode_Feild_E,
                    OBISCode_Feild_F);
                        break;

                    case FormatSpecifier.CompleteDecimalMode:
                        OBIS = String.Format("{0}:{1}.{2}.{3}.{4}.{5}.{6}",
                    ClassId,
                    OBISCode_Feild_A,
                    OBISCode_Feild_B,
                    OBISCode_Feild_C,
                    OBISCode_Feild_D,
                    OBISCode_Feild_E,
                    OBISCode_Feild_F);
                        break;

                    case FormatSpecifier.ShortHexMode:
                        OBIS = String.Format("{0:X2}.{1:X2}.{2:X2}.{3:X2}.{4:X2}.{5:X2}",
                    OBISCode_Feild_A,
                    OBISCode_Feild_B,
                    OBISCode_Feild_C,
                    OBISCode_Feild_D,
                    OBISCode_Feild_E,
                    OBISCode_Feild_F);
                        break;

                    case FormatSpecifier.ShortDecimalMode:
                        OBIS = String.Format("{0}.{1}.{2}.{3}.{4}.{5}",
                    OBISCode_Feild_A,
                    OBISCode_Feild_B,
                    OBISCode_Feild_C,
                    OBISCode_Feild_D,
                    OBISCode_Feild_E,
                    OBISCode_Feild_F);
                        break;

                    default:
                        OBIS = String.Format("{0}.{1}.{2}.{3}.{4}.{5}",
                  OBISCode_Feild_A,
                  OBISCode_Feild_B,
                  OBISCode_Feild_C,
                  OBISCode_Feild_D,
                  OBISCode_Feild_E,
                  OBISCode_Feild_F);
                        break;
                }
                return OBIS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
