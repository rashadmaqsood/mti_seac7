using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using TCP_Communication;
using ucCustomControl;
using comm;
using SmartEyeControl_7.Controllers;
using System.Xml.Serialization;
using DLMS.Comm;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_ModemLimitsAndTime))]
    public class Param_ModemLimitsAndTime : ICustomStructure,IParam
    {
        #region DataMembers

        public ushort RSSI_LEVEL_TCP_UDP_Connection;
        public ushort RSSI_LEVEL_SMS;
        public ushort RSSI_LEVEL_Data_Call;
        public ushort Time_between_Retries_SMS;
        public ushort Time_between_Retries_TCP;
        public ushort Time_between_Retries_IP_connection;
        public ushort Time_between_Retries_UDP;
        public ushort Time_between_Retries_Data_Call;
        public byte Retry_SMS;
        public byte Retry;
        public byte Retry_IP_connection;
        public byte Retry_TCP;
        public byte Retry_UDP;
        public ushort TimeRetriesAlwaysOnCycle;
        public ushort TCP_Inactivity;
        public ushort TimeOut_CipSend;
        
        #endregion

        #region Encoders/Decoders

        public void Decode_Encode_RSSI_LEVEL_TCP_UDP_Connection(Base_Class arg)
        {
            try
            {
                Class_1 RSSI_LEVEL_TCP_UDP_Connection_obj = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                RSSI_LEVEL_TCP_UDP_Connection = Convert.ToUInt16(RSSI_LEVEL_TCP_UDP_Connection_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Decode_RSSI_LEVEL_SMS(Base_Class arg)
        {
            try
            {
                Class_1 RSSI_LEVEL_SMS_obj = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                RSSI_LEVEL_SMS = Convert.ToUInt16(RSSI_LEVEL_SMS_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Decode_RSSI_LEVEL_Data_Call(Base_Class arg)
        {
            try
            {
                Class_1 RSSI_LEVEL_Data_Call_obj = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                RSSI_LEVEL_Data_Call = Convert.ToUInt16(RSSI_LEVEL_Data_Call_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Decode_Time_between_Retries_SMS(Base_Class arg)
        {
            try
            {
                Class_1 Time_between_Retries_SMS_obj = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                Time_between_Retries_SMS = Convert.ToUInt16(Time_between_Retries_SMS_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Decode_Time_between_Retries_TCP(Base_Class arg)
        {
            try
            {
                Class_1 Time_between_Retries_TCP_obj = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                Time_between_Retries_TCP = Convert.ToUInt16(Time_between_Retries_TCP_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Decode_Time_between_Retries_Data_Call(Base_Class arg)
        {
            try
            {
                Class_1 Time_between_Retries_Data_Call_obj = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                Time_between_Retries_Data_Call = Convert.ToUInt16(Time_between_Retries_Data_Call_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Decode_Time_between_Retries_IP_connection(Base_Class arg)
        {
            try
            {
                Class_1 Time_between_Retries_IP_connection_obj = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                Time_between_Retries_IP_connection = Convert.ToUInt16(Time_between_Retries_IP_connection_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Decode_Time_between_Retries_UDP(Base_Class arg)
        {
            try
            {
                Class_1 Time_between_Retries_UDP_obj = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                Time_between_Retries_UDP = Convert.ToUInt16(Time_between_Retries_UDP_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Decode_Retry_SMS(Base_Class arg)
        {
            try
            {
                Class_1 Retry_SMS_obj = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                Retry_SMS = Convert.ToByte(Retry_SMS_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Decode_Retry(Base_Class arg)
        {
            try
            {
                Class_1 Retry_obj = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                Retry = Convert.ToByte(Retry_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Decode_IP_connection(Base_Class arg)
        {
            try
            {
                Class_1 Retry_IP_connection_obj = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                Retry_IP_connection = Convert.ToByte(Retry_IP_connection_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Decode_Retry_TCP(Base_Class arg)
        {
            try
            {
                Class_1 Retry_Retry_TCP_obj = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                Retry_TCP = Convert.ToByte(Retry_Retry_TCP_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Decode_Retry_UDP(Base_Class arg)
        {
            try
            {
                Class_1 Retry_Retry_UDP_obj = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                Retry_UDP = Convert.ToByte(Retry_Retry_UDP_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

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
                byte temp = 0;
                List<byte> _encodeRaw = new List<byte>(45);
                ///Encode Modem Limits Structure Format
                _encodeRaw.AddRange( new byte[]{(byte)DataTypes._A02_structure, 17});
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.RSSI_LEVEL_TCP_UDP_Connection));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.RSSI_LEVEL_SMS));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.RSSI_LEVEL_Data_Call));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Time_between_Retries_IP_connection));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Time_between_Retries_TCP));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Time_between_Retries_UDP));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Time_between_Retries_SMS));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Time_between_Retries_Data_Call));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.TimeRetriesAlwaysOnCycle));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Retry_IP_connection));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Retry_TCP));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Retry_UDP));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Retry_SMS));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Retry));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, temp));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.TCP_Inactivity));
                _encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.TimeOut_CipSend));
                return _encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding Param_ModemLimtisAndTime", "Encode_Data", ex);
            }
            
        }

        public void Decode_Data(byte[] Data)
        {
            int arrayTraverser = 0;
            Decode_Data(Data, ref arrayTraverser,Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverser,int length)
        {
            try
            {
                byte currentByte = Data[array_traverser++];
                if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverser] != 17)
                    throw new DLMSDecodingException("Invalid ModemLimits and time Structure received", "Decode_Data_Param_Keep_Alive_IP");
                array_traverser++;
                
                this.RSSI_LEVEL_TCP_UDP_Connection = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.RSSI_LEVEL_SMS = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.RSSI_LEVEL_Data_Call = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.Time_between_Retries_IP_connection = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.Time_between_Retries_TCP = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.Time_between_Retries_UDP = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.Time_between_Retries_SMS = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.Time_between_Retries_Data_Call = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.TimeRetriesAlwaysOnCycle = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.Retry_IP_connection = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.Retry_TCP = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.Retry_UDP = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.Retry_SMS = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.Retry = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                byte temp = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.TCP_Inactivity = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
                this.TimeOut_CipSend = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverser));
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding Param_ModemLimtisAndTime", "Decode_Data", ex);
            }
        }
        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
