using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DLMS;
using DLMS.Comm;

namespace comm
{
    
    [Serializable]
    [XmlInclude(typeof(Param_ModemBasics_NEW))]
    public class Param_ModemBasics_NEW : ICustomStructure,IParam
    {
        #region Data_Members

        public string UserName;
        public string Password;
        public string WakeupPassword;
        public byte Flag_RLRQ;
        public byte Flag_DecrementCounter;
        public byte Flag_FastDisconnect;
        
        #endregion

        /// <summary>
        /// Copy Constructur
        /// </summary>
        public Param_ModemBasics_NEW(Param_ModemBasics_NEW obj)
        {
            UserName = obj.UserName;
            Password = obj.Password;
            WakeupPassword = obj.WakeupPassword;
            Flag_RLRQ = obj.Flag_RLRQ;
            Flag_DecrementCounter = obj.Flag_DecrementCounter;
            Flag_FastDisconnect = obj.Flag_FastDisconnect;
        }

        public Param_ModemBasics_NEW()
        {
            UserName = "\0";
            Password = "\0";
            WakeupPassword = "\0";
            Flag_RLRQ = 0;
            Flag_DecrementCounter = 0;
        }

        #region Decoder

        public double Decode_Any(Base_Class arg, byte Class_ID)
        {
            try
            {
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                if (Class_ID == 4)
                {
                    Class_4 temp_obj = (Class_4)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        } 

        #endregion

        #region ICustomStructure Members
        
        byte[] ICustomStructure.Encode_Data()
        {
            try
            {
                string NullString = Convert.ToChar(00).ToString();

                List<byte> encodeRaw = new List<byte>(50);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 7 });

                if (!String.IsNullOrEmpty(UserName) && !UserName.EndsWith(NullString))
                    UserName += NullString;
                if (!String.IsNullOrEmpty(Password) && !Password.EndsWith(NullString))
                    Password += NullString;
                if (!String.IsNullOrEmpty(WakeupPassword) && !WakeupPassword.EndsWith(NullString))
                    WakeupPassword += NullString;
                
                ///Unique_ID <DataType DataTypes.DataTypes._A09_octet_string>
                encodeRaw.AddRange(BasicEncodeDecode.Encode_String(this.UserName, DataTypes._A09_octet_string));
                encodeRaw.AddRange(BasicEncodeDecode.Encode_String(this.Password, DataTypes._A09_octet_string));
                encodeRaw.AddRange(BasicEncodeDecode.Encode_String(this.WakeupPassword, DataTypes._A09_octet_string));
                //IP <DataTypes._A06_double_long_unsigned>
                //IP_Profile_Flags <DataType DataTypes.DataTypes.unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Flag_RLRQ));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Flag_DecrementCounter));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Flag_FastDisconnect));  
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, 0));     //DUMMY



                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_ModemBasics", "Encode_Data_Param_ModemBasics", ex);
            }
        }

        void ICustomStructure.Decode_Data(byte[] Data)
        {
            int array_traverse = 0;
            ((ICustomStructure)this).Decode_Data(Data, ref array_traverse,Data.Length);
        }

        void ICustomStructure.Decode_Data(byte[] Data, ref int array_traverse,int length)
        {
            try
            {
                byte currentByte = Data[array_traverse++];
                if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverse] != 7)
                    throw new DLMSDecodingException("Invalid ICustomStructure Param_Modem_Basics Structure received", "Decode_Data_Param_Modem_Basics");
                array_traverse++;
                
                this.UserName = BasicEncodeDecode.Decode_String(Data, ref array_traverse);
                this.Password = BasicEncodeDecode.Decode_String(Data, ref array_traverse);
                this.WakeupPassword = BasicEncodeDecode.Decode_String(Data, ref array_traverse);
                
                this.Flag_RLRQ = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.Flag_DecrementCounter = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                 this.Flag_FastDisconnect = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                byte dummy_2 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

                if (!String.IsNullOrEmpty(UserName))
                {
                    UserName = UserName.TrimEnd(new char[] {Convert.ToChar(0)});
                }

                if (!String.IsNullOrEmpty(Password))
                {
                    Password = Password.TrimEnd(new char[] { Convert.ToChar(0) });
                }

                if (!String.IsNullOrEmpty(WakeupPassword))
                {
                    WakeupPassword = WakeupPassword.TrimEnd(new char[] { Convert.ToChar(0) });
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_Modem_Basics", "Decode_Data_Param_Modem_Basics", ex);
            }
        }

        #endregion

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return new Param_ModemBasics_NEW(this);
        }

        #endregion
    }
}
