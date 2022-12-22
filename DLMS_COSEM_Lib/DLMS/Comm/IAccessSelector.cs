using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLMS.Comm
{
    /// <summary>
    /// A parameter of the xDLMS attribute-related GET and SET services, used with logical name (LN) referencing.
    /// An attribute is fully identified by the COSEM interface class identifier, the COSEM\object instance identifier (logical name) and the attribute identifier within the given object.
    /// GET and SET services may access the whole attribute, or only a part of it (selective access). 
    /// A GET and SET service may refer to one attribute only, or several attributes. In this latter case,the GET/SET.
    /// request service includes a list of attribute descriptors.
    /// </summary>
    public interface IAccessSelector : ICloneable
    {
        SelectiveAccessType GetSelectorType();
        bool IsValid();
        byte[] Encode();
    }

    /// <summary>
    /// this class provide such a selector which Select attributes from a specified index to other specified index
    /// </summary>
    public class EntryDescripter : IAccessSelector, ICloneable
    {
        /// <summary>
        /// Max No attributes can be selected 0 means all attribute of current object
        /// </summary>
        public static readonly ushort MaxPossibleValue = 0;
        
        private uint fromEntry = 1;
        private uint toEntry = MaxPossibleValue;
        private ushort fromSelectedValue = 1;
        private ushort toSelectedValue = MaxPossibleValue;

        public EntryDescripter()
        {
            fromEntry = 1;
            toEntry = MaxPossibleValue;

            fromSelectedValue = 1;
            toSelectedValue = MaxPossibleValue;
        }

        public EntryDescripter(EntryDescripter descripter)
        {
            if (descripter == null)
                return;
            this.fromEntry = descripter.fromEntry;
            this.toEntry = descripter.toEntry;

            this.fromSelectedValue = descripter.fromSelectedValue;
            this.toSelectedValue = descripter.toSelectedValue;
        }

        /// <summary>
        /// Selection Off-set(index) from where selection must begin
        /// </summary>
        public uint FromEntry
        {
            get { return fromEntry; }
            set
            {
                fromEntry = value;
            }
        }

        
        /// <summary>
        /// To Index specify the selction must be perform till this index or where selection must ends
        /// </summary>
        public uint ToEntry
        {
            get { return toEntry; }
            set { toEntry = value; }
        }

        
        /// <summary>
        /// Selection Off-set(value) from where selection must begin
        /// </summary>
        public ushort FromSelectedValue
        {
            get { return fromSelectedValue; }
            set
            {
                if (value > 0)
                    fromSelectedValue = value;
                else
                    throw new DLMSException("Invalid From Selected Value");
            }
        }
        
        /// <summary>
        /// To Index specify the selction must be perform till this value or where selection must ends
        /// </summary>
        public ushort ToSelectedValue
        {
            get { return toSelectedValue; }
            set { toSelectedValue = value; }
        }



        #region IAccessSelector Members
        
        /// <summary>
        /// Get the current Selection type <see cref="SelectiveAccessType"/>
        /// </summary>
        /// <returns></returns>
        public SelectiveAccessType GetSelectorType()
        {
            return SelectiveAccessType.Entry_Descripter;
        }
        /// <summary>
        /// Verify the Entry Descriptor defined by the user is valid or not
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            if ((fromEntry > toEntry && toEntry != MaxPossibleValue))
                return false;
            if (fromSelectedValue > toSelectedValue && toSelectedValue != MaxPossibleValue)
                return false;
            return true;
        }
        /// <summary>
        /// Encode the EntryDescriptor Request
        /// </summary>
        /// <returns></returns>
        public byte[] Encode()
        {
            try
            {
                if (!IsValid())
                    throw new DLMSDecodingException("Unable to encode,Access + values not valid", "EntryDescripter_Encode");
                List<byte> encodeRaw = new List<byte>();
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 4 });
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A06_double_long_unsigned, fromEntry));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A06_double_long_unsigned, toEntry));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, fromSelectedValue));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, toSelectedValue));
                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSEncodingException("Unable to encode Access Selector", "EntryDescripter_Encode");
            }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return new EntryDescripter(this);
        }

        #endregion
    }
    /// <summary>
    /// Select attribute on the specified given criteria to their values e.g. select (attribute_value &gt; 10 and attribute_value &lt; 30)
    /// this Descriptor is not implemented yet
    /// </summary>
    public class RangeDescripter : IAccessSelector, ICloneable
    {
        #region Public Properties

        public object ToEntry { get; set; }
        public object FromEntry { get; set; }
        public DataTypes EncodingDataType { get; set; }
        public List<CaptureObject> Capture_Object_Definition { get; set; }

        #endregion // Public Properties

        public RangeDescripter()
        {
            ToEntry = FromEntry = null;
            EncodingDataType = DataTypes._A00_Null;
            Capture_Object_Definition = new List<CaptureObject>();
        }

        public RangeDescripter(RangeDescripter descripter)
        {
            if (descripter == null)
                return;
            ToEntry = descripter.ToEntry;
            FromEntry = descripter.FromEntry;

            EncodingDataType = descripter.EncodingDataType;
        }

        #region IAccessSelector Members

        public SelectiveAccessType GetSelectorType()
        {
            return SelectiveAccessType.Range_Descripter;
        }

        public byte[] Encode()
        {
            try
            {
                if (!IsValid())
                    throw new DLMSDecodingException("Unable to encode,Access + values not valid", "RangeDescripter_Encode");

                byte[] EncodeData = null;

                List<byte> encodeRaw = new List<byte>();
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 4 });

                DateTime fromDate = DateTime.Now;
                DateTime ToDate = DateTime.Now;

                switch (EncodingDataType)
                {
                    case DataTypes._A19_datetime:
                        {
                            //Structure of Four Elements
                            encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 4 });

                            // Element 1
                            StOBISCode MeterClock = Get_Index.Meter_Clock;
                            encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, MeterClock.ClassId));
                            encodeRaw.AddRange(BasicEncodeDecode.Encode_OctetString(MeterClock.OBISCode, DataTypes._A09_octet_string));
                            encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A0F_integer, 2));
                            encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, 0));

                            fromDate = (DateTime)FromEntry;
                            ToDate = (DateTime)ToEntry;
                            // Element 2 From Date
                            BasicEncodeDecode.Encode_DateTime(fromDate, ref EncodeData);
                            encodeRaw.AddRange(BasicEncodeDecode.Encode_OctetString(EncodeData, DataTypes._A09_octet_string));
                            // Element 3 To Date
                            BasicEncodeDecode.Encode_DateTime(ToDate, ref EncodeData);
                            encodeRaw.AddRange(BasicEncodeDecode.Encode_OctetString(EncodeData, DataTypes._A09_octet_string));
                            // Element 3 Array of CaptureObjects
                            encodeRaw.Add((byte)DataTypes._A01_array);
                            byte[] ArrayLength = null;
                            BasicEncodeDecode.Encode_Length(ref ArrayLength, (ushort)Capture_Object_Definition.Count);
                            encodeRaw.AddRange(ArrayLength);

                            foreach (CaptureObject bClass in Capture_Object_Definition)
                            {
                                encodeRaw.AddRange(Class_7.EncodeCaptureObject(bClass));
                            }

                            break;
                        }
                    case DataTypes._A1A_date:
                        {
                            fromDate = (DateTime)FromEntry;
                            ToDate = (DateTime)ToEntry;

                            BasicEncodeDecode.Encode_Date(fromDate, ref EncodeData);
                            encodeRaw.AddRange(EncodeData);

                            BasicEncodeDecode.Encode_Date(ToDate, ref EncodeData);
                            encodeRaw.AddRange(EncodeData);
                            break;
                        }
                    case DataTypes._A1B_time:
                        {
                            fromDate = (DateTime)FromEntry;
                            ToDate = (DateTime)ToEntry;

                            BasicEncodeDecode.Encode_Time(fromDate, ref EncodeData);
                            encodeRaw.AddRange(EncodeData);

                            BasicEncodeDecode.Encode_Time(ToDate, ref EncodeData);
                            encodeRaw.AddRange(EncodeData);
                            break;
                        }
                    // TODO: encode separately
                    case DataTypes._A09_octet_string:
                    case DataTypes._A0A_visible_string:
                        {
                            break;
                        }
                    // TODO: verify is it utf8-string? 
                    case DataTypes._A05_double_long:
                    case DataTypes._A06_double_long_unsigned:
                    case DataTypes._A0F_integer:
                    case DataTypes._A10_long:
                    case DataTypes._A11_unsigned:
                    case DataTypes._A12_long_unsigned:
                    case DataTypes._A14_long_64:
                    case DataTypes._A15_long_64_unsigned:
                    case DataTypes._A23_Float32:
                    case DataTypes._A24_Float64:
                        {
                            encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(EncodingDataType, (ValueType)FromEntry));
                            encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(EncodingDataType, (ValueType)ToEntry));
                            break;
                        }
                }
                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException)
                    throw ex;
                else
                    throw new DLMSEncodingException("Unable to encode Access Selector", "RangeDescripter_Encode");
            }
        }

        public bool IsValid()
        {
            DateTime fromDate = DateTime.Now;
            DateTime ToDate = DateTime.Now;
            bool result = false;
            try
            {
                switch (EncodingDataType)
                {
                    case DataTypes._A19_datetime:
                    case DataTypes._A1A_date:
                    case DataTypes._A1B_time:
                        {
                            fromDate = (DateTime)FromEntry;
                            ToDate = (DateTime)ToEntry;
                            result = (fromDate > DateTime.MinValue && ToDate < DateTime.MaxValue);
                            break;
                        }

                    case DataTypes._A09_octet_string:
                    case DataTypes._A0A_visible_string:
                        {
                            string fromStr = FromEntry as string;
                            string toStr = ToEntry as string;
                            result = (!String.IsNullOrEmpty(fromStr) && !String.IsNullOrEmpty(toStr));
                            break;
                        }
                    // TODO: verify is it utf8-string? 
                    case DataTypes._A05_double_long:
                    case DataTypes._A06_double_long_unsigned:
                    case DataTypes._A0F_integer:
                    case DataTypes._A10_long:
                    case DataTypes._A11_unsigned:
                    case DataTypes._A12_long_unsigned:
                    case DataTypes._A14_long_64:
                    case DataTypes._A15_long_64_unsigned:
                    case DataTypes._A23_Float32:
                    case DataTypes._A24_Float64:
                        {
                            //TODO
                            result = true;
                            break;
                        }
                }
            }
            catch (Exception)
            {
                throw new DLMSEncodingException("Unable to validate Access Selector", "RangeDiscriptor_validate");
            }
            return result;
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        #endregion
    }
    /// <summary>
    /// Specify the Selective Access Type
    /// </summary>
    public enum SelectiveAccessType : byte
    {
        /// <summary>
        /// Flat Retrival
        /// </summary>
        Not_Applied = 0,
        /// <summary>
        /// Select attribute on the specified given criteria to thier values e.g. select (attribute_value &gt; 10 and attribute_value &lt; 30)
        /// </summary>
        Range_Descripter = 1,
        /// <summary>
        /// Select attributes from specified index to other specified index
        /// </summary>
        Entry_Descripter = 2,
        /// <summary>
        /// hybrid 
        /// </summary>
        Both_Types = 3
    }

}
