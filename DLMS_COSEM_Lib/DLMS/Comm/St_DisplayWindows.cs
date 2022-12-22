using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLMS.Comm
{
    /// <summary>
    /// This structure is defined to customaize the display window of a meter to show data quantities.
    /// This will use to store the DisplayWindow Items which is specificaly coustom display for a Meter Display window
    /// </summary>
    public class St_DisplayWindows : ICustomStructure
    {
        #region DataMembers
        
        private byte scroll_Time;
        private St_WindowItem[] displayWindows;

        #endregion

        #region Constructures
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Scroll_Time">scroll time or sliding time for each display window item in items collection</param>
        /// <param name="Windows">Display window Item Collection</param>
        public St_DisplayWindows(byte Scroll_Time, St_WindowItem[] Windows)
        {
            this.scroll_Time = Scroll_Time;
            displayWindows = Windows;
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public St_DisplayWindows()
        {
            scroll_Time = 0;
            displayWindows = null;
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="Windows"></param>
        public St_DisplayWindows(St_DisplayWindows Windows)
        {
            this.scroll_Time = Windows.scroll_Time;
            this.displayWindows = Windows.displayWindows;
        }
        #endregion

        #region Properties
        public byte Scroll_Time
        {
            get { return scroll_Time; }
            set { scroll_Time = value; }
        }

        public St_WindowItem[] DisplayWindows
        {
            get { return displayWindows; }
            set { displayWindows = value; }
        }
        #endregion

        #region ICustomStructure Members
        /// <summary>
        /// Encodes the user defined Display windows set parameter request
        /// </summary>
        /// <returns></returns>
        byte[] ICustomStructure.Encode_Data()
        {
            try
            {
                List<byte> RawData = new List<byte>();
                //Encode Data Type Structure _A02_structure
                RawData.Add((byte)DataTypes._A02_structure);
                //Encode Structure Length 0x02
                RawData.Add(2);
                //Encode Scroll Time Data Type _A11_unsigned
                RawData.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Scroll_Time));

                //Encode Array Of St_WindowItem Objects
                //Encode Data Type  _A01_array
                RawData.Add((byte)DataTypes._A01_array);
                //Encode Array Length
                byte[] Length_Bytes = null;
                BasicEncodeDecode.Encode_Length(ref Length_Bytes, (ushort)this.DisplayWindows.Length);
                RawData.AddRange(Length_Bytes);
                foreach (var item in DisplayWindows)
                {
                    RawData.AddRange(((ICustomStructure)item).Encode_Data());
                }
                return RawData.ToArray();

            }
            catch (Exception ex)
            {
                throw new DLMSEncodingException(String.Format("Error Encoding St_DisplayWindows Structure (Error Code:{0})", (int)DLMSErrors.ErrorEncoding_Type), "Encode_Data", ex);
            }
        }

        void ICustomStructure.Decode_Data(byte[] Data)
        {
            int array_traverse = 0;
            ((ICustomStructure)this).Decode_Data(Data, ref array_traverse, Data.Length);
        }
        /// <summary>
        /// Decode the Meter Display windows Set request response (meter display window setting)
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="array_traverse"></param>
        /// <param name="length"></param>
        void ICustomStructure.Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                byte currentChar = Data[array_traverse++];
                /// Validate Structure
                if (currentChar != (byte)DataTypes._A02_structure || Data[array_traverse++] != 2)
                    throw new DLMSDecodingException("Invalid St_DisplayWindows Structure format", "Decode_Data_ICustomStructure");

                /// Decode Scroll Time Data Type _A11_unsigned
                Scroll_Time = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));

                /// Decode Array Of St_WindowItem
                currentChar = Data[array_traverse++];
                if (currentChar != (byte)DataTypes._A01_array)
                    throw new DLMSDecodingException(String.Format("Error Decoding St_DisplayWindows Structure Array Type expected"),
                                                                  "Decode_Data_ICustomStructure");

                int windowCount = BasicEncodeDecode.Decode_Length(Data, ref array_traverse, Data.Length);
                DisplayWindows = new St_WindowItem[windowCount];

                /// Decode St_WindowItem Structures Items
                for (int index = 0; index < windowCount; index++)
                {
                    St_WindowItem WindowItem = new St_WindowItem();
                    ((ICustomStructure)WindowItem).Decode_Data(Data, ref array_traverse, Data.Length);
                    DisplayWindows[index] = WindowItem;
                }
            }
            catch (Exception ex)
            {
                throw new DLMSDecodingException(String.Format("Error Decoding St_DisplayWindows Structure (Error Code:{0})",
                                               (int)DLMSErrors.ErrorDecoding_Type), "Decode_Data_ICustomStructure", ex);
            }
        }

        #endregion

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return new St_DisplayWindows(this);
        }

        #endregion
    }
    /// <summary>
    /// This structure is used for the customization of a single display window for a particular interval in Meter
    /// </summary>
    public class St_WindowItem : ICustomStructure
    {
        #region Data_Members
        private byte attributeNo;
        private byte[] OBIS_Code;
        private ushort winNumberToDisplay;
        #endregion

        #region Structures
        /// <summary>
        /// Defualt Constructor
        /// </summary>
        public St_WindowItem()
        {
            attributeNo = 0;
            OBIS_Code = null;
            winNumberToDisplay = 0;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="attributeNo">particulat attribute of a particular object to display</param>
        /// <param name="OBISCode">OBIS Code fot Data Quantity/Attribute to display </param>
        /// <param name="WinNumber">no in sequence</param>
        public St_WindowItem(byte attributeNo, byte[] OBISCode, ushort WinNumber)
        {
            attributeNo = attributeNo;
            this.OBIS_Code = OBISCode;
            this.winNumberToDisplay = WinNumber;
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="WinObj">Onject to Copy</param>
        public St_WindowItem(St_WindowItem WinObj)
        {
            this.attributeNo = WinObj.attributeNo;
            this.OBIS_Code = WinObj.OBIS_Code;
            this.winNumberToDisplay = WinObj.winNumberToDisplay;
        }
        #endregion

        #region Properties
        /// <summary>
        /// GET/SET the Attribute no whom user wants to display
        /// </summary>
        public byte AttributeNo
        {
            get { return attributeNo; }
            set { attributeNo = value; }
        }
        /// <summary>
        /// GET/SET the OBIS code for displaying data quantity/ attribute
        /// </summary>
        public byte[] OBISCode
        {
            get { return OBIS_Code; }
            set { OBIS_Code = value; }
        }
        /// <summary>
        /// Sequence no to display this window
        /// </summary>
        public ushort WinNumberToDisplay
        {
            get { return winNumberToDisplay; }
            set { winNumberToDisplay = value; }
        }
        #endregion

        #region ICustomStructure Members
        /// <summary>
        /// Encode the set Display Window Request
        /// </summary>
        /// <returns></returns>
        byte[] ICustomStructure.Encode_Data()
        {
            try
            {
                List<byte> RawData = new List<byte>();
                /// Encode Data Type Structure _A02_structure
                RawData.Add((byte)DataTypes._A02_structure);
                /// Encode Structure Length 0x03
                RawData.Add(3);
                /// Encode Attribute ID Data Type _A0F_integer
                RawData.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A0F_integer, AttributeNo));
                /// Encode OBIS Code Data Type _A09_octet_string
                RawData.AddRange(BasicEncodeDecode.Encode_OctetString(OBISCode, DataTypes._A09_octet_string));
                /// Encode WinNumberToDisplay Data Type _A12_long_unsigned
                RawData.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, WinNumberToDisplay));
                return RawData.ToArray();
            }
            catch (Exception ex)
            {
                throw new DLMSEncodingException(String.Format("Error Encoding St_WindowItem Structure (Error Code:{0})",
                                                                             (int)DLMSErrors.ErrorEncoding_Type), "Encode_Data", ex);
            }
        }

        void ICustomStructure.Decode_Data(byte[] Data)
        {
            int array_trverse = 0;
            ((ICustomStructure)this).Decode_Data(Data, ref array_trverse, Data.Length);
        }
        /// <summary>
        /// Decode the set window request response
        /// </summary>
        /// <param name="Data">Response</param>
        /// <param name="array_traverse">offset</param>
        /// <param name="length">Length</param>
        void ICustomStructure.Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                byte currentChar = Data[array_traverse++];
                //Validate Structure
                if (currentChar != (byte)DataTypes._A02_structure || Data[array_traverse++] != 3)
                    throw new DLMSDecodingException("Invalid St_WindowItem Structure format", "Decode_Data_ICustomStructure");
                //Decode Attribue ID Data Type _A0F_integer
                AttributeNo = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                //Decode OBIS Code Data Type _A09_octet_string
                OBISCode = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
                //Decode WinNumberToDisplay Data Type _A12_long_unsigned
                WinNumberToDisplay = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
            }
            catch (Exception ex)
            {
                throw new DLMSDecodingException(String.Format("Error Decoding St_WindowItem Structure (Error Code:{0})", (int)DLMSErrors.ErrorDecoding_Type), "Decode_Data", ex);

            }
        }

        #endregion

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return new St_WindowItem(this);
        }

        #endregion
    }


}
