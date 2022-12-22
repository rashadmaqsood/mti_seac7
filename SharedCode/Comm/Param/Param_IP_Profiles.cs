using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;
using DLMS;
using DLMS.Comm;
using System.ComponentModel;

namespace SharedCode.Comm.Param
{
    [XmlInclude(typeof(Param_Standard_IP_Profile))]
    [Serializable]
    public class Param_Standard_IP_Profile:IParam
    {
        #region Data_Members

        [XmlElement("IP", Type = typeof(uint))]
        public uint IP;

        [XmlElement("Wrapper_Over_TCP_port", Type = typeof(ushort))]
        public ushort Wrapper_Over_TCP_port;

        public byte[] Destination 
        { 
            get
            {
                byte[] IP_Bytes = BitConverter.GetBytes(IP);
                byte[] Port_Bytes = BitConverter.GetBytes(Wrapper_Over_TCP_port);
                byte[] DestinationBytes = new byte[6];
                Array.Copy(IP_Bytes, 0, DestinationBytes, 0, IP_Bytes.Length);
                Array.Copy(Port_Bytes, 0, DestinationBytes, IP_Bytes.Length, Port_Bytes.Length);
                return DestinationBytes;

            }
        }
        #endregion

        public Param_Standard_IP_Profile()
        {
            IP = Convert.ToUInt32(IPAddress.Any.AddressFamily);
            Wrapper_Over_TCP_port = 4060;
        }

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

    }
    [XmlInclude(typeof(Param_IP_Profiles))]
    [Serializable]
    public class Param_IP_Profiles:Param_Standard_IP_Profile ,ICustomStructure, IParam, ICloneable
    {
        #region Data_Members
        [XmlElement("Unique_ID", Type = typeof(byte))]
        public byte Unique_ID;
        //[XmlElement("IP", Type = typeof(uint))]
        //public uint IP;
        //[XmlElement("Wrapper_Over_TCP_port", Type = typeof(ushort))]
        //public ushort Wrapper_Over_TCP_port;
        [XmlElement("Wrapper_Over_UDP_port", Type = typeof(ushort))]
        public ushort Wrapper_Over_UDP_port;
        [XmlElement("HDLC_Over_TCP_Port", Type = typeof(ushort))]
        public ushort HDLC_Over_TCP_Port;
        [XmlElement("HDLC_Over_UDP_Port", Type = typeof(ushort))]
        public ushort HDLC_Over_UDP_Port;
        /// <summary>
        /// Reserved_Flags
        /// </summary>
        #region RawFlags
        [XmlElement("RawFlags", Type = typeof(byte))]
        public byte RawFlags
        {
            get
            {
                byte Flags = 0x00;
                Flags += (Reserved_Flag_7) ? (byte)0x80 : (byte)0x00;
                Flags += (Reserved_Flag_6) ? (byte)0x40 : (byte)0x00;
                Flags += (Reserved_Flag_5) ? (byte)0x20 : (byte)0x00;
                Flags += (Reserved_Flag_4) ? (byte)0x10 : (byte)0x00;
                Flags += (Reserved_Flag_3) ? (byte)0x08 : (byte)0x00;
                Flags += (Reserved_Flag_2) ? (byte)0x04 : (byte)0x00;
                Flags += (Reserved_Flag_1) ? (byte)0x02 : (byte)0x00;
                Flags += (Reserved_Flag_0) ? (byte)0x01 : (byte)0x00;
                return Flags;
            }
            set
            {
                ///Decoding Flags Byte
                byte Flags = value;
                Reserved_Flag_7 = ((Flags & 0x80) > 0) ? true : false;
                Reserved_Flag_6 = ((Flags & 0x40) > 0) ? true : false;
                Reserved_Flag_5 = ((Flags & 0x20) > 0) ? true : false;
                Reserved_Flag_4 = ((Flags & 0x10) > 0) ? true : false;
                Reserved_Flag_3 = ((Flags & 0x08) > 0) ? true : false;
                Reserved_Flag_2 = ((Flags & 0x04) > 0) ? true : false;
                Reserved_Flag_1 = ((Flags & 0x02) > 0) ? true : false;
                Reserved_Flag_0 = ((Flags & 0x01) > 0) ? true : false;
            }
        }
        #endregion
        [XmlIgnore()]
        public bool Reserved_Flag_0;
        [XmlIgnore()]
        public bool Reserved_Flag_1;
        [XmlIgnore()]
        public bool Reserved_Flag_2;
        [XmlIgnore()]
        public bool Reserved_Flag_3;
        [XmlIgnore()]
        public bool Reserved_Flag_4;
        [XmlIgnore()]
        public bool Reserved_Flag_5;
        [XmlIgnore()]
        public bool Reserved_Flag_6;
        [XmlIgnore()]
        public bool Reserved_Flag_7;
        #endregion

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

        #region ICustomStructure Members

        public byte[] Encode_Data()
        {
            try
            {
                List<byte> encodeRaw = new List<byte>(50);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 7 });
                ///Unique_ID <DataType DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Unique_ID));
                //IP <DataTypes._A06_double_long_unsigned>
                //IP_Profile_Flags <DataType DataTypes.DataTypes.unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, RawFlags));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A06_double_long_unsigned, this.IP));
                ///Wrapper_Over_TCP_port <DataType DataTypes.>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Wrapper_Over_TCP_port));
                ///Wrapper_Over_UDP_port <DataType DataTypes.DataTypes._A12_long_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Wrapper_Over_UDP_port));
                ///HDLC_Over_TCP_port <DataType DataTypes.DataTypes._A12_long_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.HDLC_Over_TCP_Port));
                ///HDLC_Over_UDP_port <DataType DataTypes.DataTypes._A12_long_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.HDLC_Over_UDP_Port));

                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_IP_Profiles", "Encode_Data_Param_IP_Profiles", ex);
            }
        }

        public void Decode_Data(byte[] Data)
        {
            int array_traverse = 0;
            Decode_Data(Data, ref array_traverse,Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse,int length)
        {
            try
            {
                byte currentByte = Data[array_traverse++];
                if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverse] != 7)
                    throw new DLMSDecodingException("Invalid ICustomStructure Param_IP_Profiles Structure received", "Decode_Data_Param_IP_Profiles");
                array_traverse++;
                ///IP_Profiles <DataType DataTypes._A11_unsigned>
                this.Unique_ID = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                //IP_Profile_Flags <DataType DataTypes.DataTypes.unsigned>
                RawFlags = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                //IP <DataTypes._A06_double_long_unsigned>
                this.IP = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///Wrapper_Over_TCP_port <DataType DataTypes.DataTypes._A12_long_unsigned>
                this.Wrapper_Over_TCP_port = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///Wrapper_Over_UDP_port <DataType DataTypes.DataTypes._A12_long_unsigned>
                this.Wrapper_Over_UDP_port = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///HDLC_Over_TCP_port <DataType DataTypes.DataTypes._A12_long_unsigned>
                this.HDLC_Over_TCP_Port = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///HDLC_Over_UDP_port <DataType DataTypes.DataTypes._A12_long_unsigned>
                this.HDLC_Over_UDP_Port = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_IP_Profiles", "Decode_Data_Param_IP_Profiles", ex);
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

    public class Param_IP_ProfilesHelper : INotifyPropertyChanged
    {
        public static int Max_IP_Profile = 04;
        public static String PropertyName = "Param_IP_Profiles_object";
        private int previous_Total_IP_profiles = 0;
        private Param_IP_Profiles[] _Param_IP_Profiles_object = null;

        public Param_IP_Profiles[] Param_IP_Profiles_object
        {
            get { return _Param_IP_Profiles_object; }
            set { _Param_IP_Profiles_object = value;}
        }
        public int Previous_Total_IP_profiles
        {
            get { return previous_Total_IP_profiles; }
            set { previous_Total_IP_profiles = value; }
        }
        public int Total_IP_Profile
        {
            get
            {
                int count = 0;
                foreach (var ipParam in _Param_IP_Profiles_object)
                {
                    if (ipParam != null && count < ipParam.Unique_ID)
                        count = ipParam.Unique_ID;
                }
                return count;
            }
        }

        #region Constructor

        public Param_IP_ProfilesHelper()
        {
            PropertyChanged += new PropertyChangedEventHandler((object sender, PropertyChangedEventArgs property) => { });
            _Param_IP_Profiles_object = new Param_IP_Profiles[Max_IP_Profile];
            for (int index = 0; index < Max_IP_Profile; index++)
                _Param_IP_Profiles_object[index] = new Param_IP_Profiles()
                {
                    Unique_ID = 0,
                    IP = 0,
                    HDLC_Over_TCP_Port = 0,
                    HDLC_Over_UDP_Port = 0,
                    Wrapper_Over_TCP_port = 4059,
                    Wrapper_Over_UDP_port = 4059
                };

        }

        public Param_IP_ProfilesHelper(Param_IP_Profiles[] Param_IP_Profiles_objectArg)
        {
            PropertyChanged += new PropertyChangedEventHandler((object sender, PropertyChangedEventArgs property) => { });
            ///_Param_IP_Profiles_object = new Param_IP_Profiles[Max_IP_Profile];
            //for (int index = 0; index < Max_IP_Profile; index++)
            ///    _Param_IP_Profiles_object[index] = Param_IP_Profiles_objectArg[index];
            _Param_IP_Profiles_object = Param_IP_Profiles_objectArg;
        }

        #endregion

        #region AddParam_IP_Profiles

        public Param_IP_Profiles AddParam_IP_Profiles(Param_IP_Profiles Arg)
        {
            byte total_IP_Profile_Count = (byte)Total_IP_Profile;
            if (total_IP_Profile_Count < Max_IP_Profile)
            {
                _Param_IP_Profiles_object[total_IP_Profile_Count] = Arg;
                Arg.Unique_ID = (byte)(total_IP_Profile_Count + 1);
            }
            else
                throw new Exception(String.Format("Unable to Add IP Profile {0}", Arg));
            OnPropertyChanged(PropertyName);
            return Arg;
        }

        public Param_IP_Profiles AddParam_IP_Profiles()
        {
            Param_IP_Profiles Param_IP_Profile = new Param_IP_Profiles()
            {
                Unique_ID = 0,
                IP = 0,
                HDLC_Over_TCP_Port = 0,
                HDLC_Over_UDP_Port = 0,
                Wrapper_Over_TCP_port = 4059,
                Wrapper_Over_UDP_port = 4059
            };
            return AddParam_IP_Profiles(Param_IP_Profile);
        }

        public Param_IP_Profiles GetLastParam_IP_Profiles()
        {
            Param_IP_Profiles Param_IP_Profile = null;
            byte total_IP_Profile_Count = (byte)Total_IP_Profile;
            if (total_IP_Profile_Count > 0)
                Param_IP_Profile = _Param_IP_Profiles_object[total_IP_Profile_Count - 1];
            return Param_IP_Profile;
        }

        #endregion

        #region RemoveParam_IP_Profiles

        public Param_IP_Profiles RemoveParam_IP_Profiles()
        {
            try
            {
                Param_IP_Profiles Arg = null;
                byte total_IP_Profile_Count = (byte)Total_IP_Profile;
                if (total_IP_Profile_Count > 0)
                {
                    Arg = _Param_IP_Profiles_object[total_IP_Profile_Count - 1];
                    if (Arg != null)
                        Arg.Unique_ID = 0;
                }
                else
                    throw new Exception(String.Format("Unable to Remove IP Profile {0}", Arg));
                OnPropertyChanged(PropertyName);
                return Arg;
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException)
                    throw new Exception(String.Format("Unable to Remove IP Profile {0}", ex.Message), ex);
                else
                    throw ex;
            }
        }

        #endregion

        public static Param_IP_Profiles[] Param_IP_Profiles_initailze(int Instances)
        {
            Param_IP_Profiles[] Param_IP_Profiles_object = new Param_IP_Profiles[Instances];
            int count;
            for (count = 0; count < Instances; count++)
            {
                Param_IP_Profiles_object[count] = new Param_IP_Profiles();
            }
            return Param_IP_Profiles_object;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                ///Raising the event when FirstName or LastName property value changed
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Param_Standard_IP_ProfilesHelper : INotifyPropertyChanged
    {
        public static int Max_Standard_IP_Profile = 04;
        public static String PropertyName = "Param_Standard_IP_Profiles_object";

        public static Param_Standard_IP_Profile[] Param_Standard_IP_Profiles_initailze(int Instances)
        {
            Param_Standard_IP_Profile[] Param_IP_Profiles_object = new Param_Standard_IP_Profile[Instances];
            int count;
            for (count = 0; count < Instances; count++)
            {
                Param_IP_Profiles_object[count] = new Param_Standard_IP_Profile();
            }
            return Param_IP_Profiles_object;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                ///Raising the event when FirstName or LastName property value changed
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
