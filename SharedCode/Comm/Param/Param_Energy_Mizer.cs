using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SharedCode.Comm.DataContainer;
using DLMS.Comm;
using SharedCode.Comm.HelperClasses;
using System.Net;
using System.Security.Permissions;
using System.Runtime.Serialization;


namespace SharedCode.Comm.Param
{
    //TODO: SaveToFile 01 create required Param Class
    [Serializable]
    [XmlInclude(typeof(Param_Energy_Mizer))]
    public class Param_Energy_Mizer : ISerializable, ICloneable, IParam
    {
        //TODO:01 DataType Should Change effect For EnergyMizer
        #region -- Properties --

        [XmlElement("RFChannel", Type = typeof(uint))]
        public uint RFChannel { get; set; }

        [XmlElement("ChannelFilterWB", Type = typeof(double))]
        public double ChannelFilterWB { get; set; }
        
        [XmlElement("TransmitCarrierFreq", Type = typeof(double))]
        public double TransmitCarrierFreq { get; set; }

        [XmlElement("ReceiveCarrierFreq", Type = typeof(double))]
        public double ReceiveCarrierFreq { get; set; }
        
        [XmlElement("RFBaudRate", Type = typeof(uint))]
        public uint RFBaudRate{ get; set; }

        [XmlElement("RFPower", Type = typeof(int))]
        public int RFPower{ get; set; }

        [XmlElement("PacketMode", Type = typeof(byte))]
        public byte PacketMode { get; set; }

        [XmlElement("FrequencyDeviation", Type = typeof(uint))]
        public uint FrequencyDeviation { get; set; }

        [XmlElement("ReceiverBandwidth", Type = typeof(uint))]
        public uint ReceiverBandwidth { get; set; }

        [XmlElement("Preamble", Type = typeof(uint))]
        public uint Preamble { get; set; }

        [XmlElement("SyncWord", Type = typeof(uint))]
        public uint SyncWord { get; set; }

        [XmlElement("AddressFiltering", Type = typeof(bool))]
        public bool AddressFiltering { get; set; }

        [XmlElement("NodeAddress", Type = typeof(uint))]
        public uint NodeAddress{ get; set; }

        [XmlElement("BroadcastAddress", Type = typeof(uint))]
        public uint BroadcastAddress { get; set; }

        [XmlElement("AESEncryption", Type = typeof(bool))]
        public bool AESEncryption { get; set; }

        [XmlElement("RFCommandDelay", Type = typeof(uint))]
        public uint RFCommandDelay { get; set; }

        [XmlElement("RFCommandTimeout", Type = typeof(uint))]
        public uint RFCommandTimeout { get; set; }

        [XmlElement("ModulationType", Type = typeof(byte))]
        public byte ModulationType { get; set; }

        [XmlElement("PacketEncoding", Type = typeof(bool))]
        public bool PacketEncoding { get; set; }

        [XmlElement("CostParameters", Type = typeof(double))]
        public double CostParameters { get; set; }

        [XmlElement("Temperature", Type = typeof(uint))]
        public uint Temperature { get; set; }

        [XmlElement("WiFiWebServerConfigurationIP", Type = typeof(IPAddress))]
        public IPAddress WiFiWebServerConfigurationIP { get; set; }

        [XmlElement("WiFiWebServerConfigurationPort", Type = typeof(uint))]
        public uint WiFiWebServerConfigurationPort { get; set; }

        [XmlElement("WiFiServerIP", Type = typeof(IPAddress))]
        public IPAddress WiFiServerIP { get; set; }

        [XmlElement("WiFiServerPort", Type = typeof(ushort))]
        public ushort WiFiServerPort { get; set; }
        //public ---- WiFiBasicConfiguration { get; set; }

        [XmlElement("WiFiClientIP", Type = typeof(IPAddress))]
        public IPAddress WiFiClientIP { get; set; }

        [XmlElement("WiFiClientPort", Type = typeof(uint))]
        public uint WiFiClientPort { get; set; }
        
        [XmlElement("SerialNumber", Type = typeof(uint))]
        public uint SerialNumber { get; set; }

        [XmlElement("LCDContrast", Type = typeof(uint))]
        public uint LCDContrast { get; set; }
        //ModemParameter

        [XmlElement("MeterToReadType", Type = typeof(byte))]
        public byte MeterToReadType { get; set; }

        [XmlElement("MeterToRead", Type = typeof(byte[]))]
        public byte[] MeterToRead { get; set; }

        [XmlElement("MeterPassword", Type = typeof(byte[]))]
        public byte[] MeterPassword { get; set; }
        //public ---- DataToRead { get; set; }
        //public ---- DisplayWindows { get; set; }

        [XmlElement("BuzzerSettings", Type = typeof(bool))]
        public bool BuzzerSettings { get; set; }

        [XmlElement("ReadHumidity", Type = typeof(bool))]
        public bool ReadHumidity { get; set; }

        [XmlElement("TempratureSettings", Type = typeof(byte))]
        public byte TempratureSettings { get; set; }

        [XmlElement("USBParams", Type = typeof(byte))]
        public byte USBParams { get; set; }

        [XmlElement("WiFiSSID", Type = typeof(byte[]))]
        public byte[] WiFiSSID { get; set; }

        [XmlElement("WiFiPassword", Type = typeof(byte[]))]
        public byte[] WiFiPassword { get; set; }
        #endregion //-- Properties --

        public Param_Energy_Mizer()
        { }

        #region ISerializable interface Members

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public Param_Energy_Mizer(SerializationInfo info, StreamingContext context)
        {
            //TODO:02 DataType Should Change effect For EnergyMizer

            //this.RFChannel = (uint)info.GetValue("RFChannel", typeof(uint));
            //this.ChannelFilterWB = (double)info.GetValue("ChannelFilterWB", typeof(double));
            //this.ReceiveCarrierFreq = (double)info.GetValue("ReceiveCarrierFrequency", typeof(double));
            //this.TransmitCarrierFreq = (double)info.GetValue("TransmitCarrierFrequency", typeof(double));
            //this.RFBaudRate = (uint)info.GetValue("RFBaudRate", typeof(uint));
            //this.RFPower = (int)info.GetValue("RFPower", typeof(int));
            //this.PacketMode = (byte)info.GetValue("PacketMode", typeof(byte));
            //this.FrequencyDeviation = (uint)info.GetValue("FrequencyDeviation", typeof(uint));
            //this.ReceiverBandwidth = (uint)info.GetValue("ReceiverBandwidth", typeof(uint));
            //this.Preamble = (uint)info.GetValue("Preamble", typeof(uint));
            //this.SyncWord = (uint)info.GetValue("SyncWord", typeof(uint));
            //this.AddressFiltering = (bool)info.GetValue("AddressFiltering", typeof(bool));
            //this.NodeAddress = (uint)info.GetValue("NodeAddress", typeof(uint));
            //this.BroadcastAddress = (uint)info.GetValue("BroadcastAddress", typeof(uint));
            //this.AESEncryption = (bool)info.GetValue("AESEncryption", typeof(bool));
            //this.RFCommandDelay = (uint)info.GetValue("RFCommandDelay", typeof(uint));
            //this.RFCommandTimeout = (uint)info.GetValue("RFCommandTimeout", typeof(uint));
            //this.ModulationType = (byte)info.GetValue("ModulationType", typeof(byte));
            //this.PacketEncoding = (bool)info.GetValue("PacketEncoding", typeof(bool));
            //this.CostParameters = (double)info.GetValue("CostParameters", typeof(double));
            //this.Temperature = (uint)info.GetValue("Temperature", typeof(uint));

            //this.WiFiWebServerConfigurationIP = (IPAddress)info.GetValue("WiFiWebServerConfigurationIP", typeof(IPAddress));
            //this.WiFiWebServerConfigurationPort = (uint)info.GetValue("WiFiWebServerConfigurationPort", typeof(uint));
            //this.WiFiServerIP = (IPAddress)info.GetValue("WiFiServerIP", typeof(IPAddress));
            //this.WiFiServerPort = (ushort)info.GetValue("WiFiServerPort", typeof(ushort));

            //this.WiFiClientIP = (IPAddress)info.GetValue("WiFiClientIP", typeof(IPAddress));
            //this.WiFiClientPort = (uint)info.GetValue("WiFiClientPort", typeof(uint));

            //this.SerialNumber = (uint)info.GetValue("SerialNumber", typeof(uint));
            //this.LCDContrast = (uint)info.GetValue("LCDContrast", typeof(uint));
            //this.MeterToReadType = (byte)info.GetValue("MeterToReadType", typeof(byte));
            //this.MeterToRead = (byte[])info.GetValue("MeterToRead", typeof(byte[]));
            //this.MeterPassword = (byte[])info.GetValue("MeterPassword", typeof(byte[]));

            //this.BuzzerSettings = (bool)info.GetValue("BuzzerSettings", typeof(bool));
            //this.ReadHumidity = (bool)info.GetValue("ReadHumidity", typeof(bool));
            //this.TempratureSettings = (byte)info.GetValue("TempratureSettings", typeof(byte));
            //this.USBParams = (byte)info.GetValue("USBParams", typeof(byte));

            //this.WiFiSSID = (byte[])info.GetValue("WiFiSSID", typeof(byte[]));
            //this.WiFiPassword = (byte[])info.GetValue("WiFiPassword", typeof(byte[]));

            //this. = ()info.GetValue("", typeof());

        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //TODO:03 DataType Should Change effect For EnergyMizer
            try
            {
                info.AddValue("RFChannel", this.RFChannel, typeof(uint));
                info.AddValue("ChannelFilterWB", this.ChannelFilterWB, typeof(double));
                info.AddValue("TransmitCarrierFrequency", this.TransmitCarrierFreq, typeof(double));
                info.AddValue("ReceiveCarrierFrequency", this.ReceiveCarrierFreq, typeof(double));
                info.AddValue("RFBaudRate", this.RFBaudRate, typeof(uint));
                info.AddValue("RFPower", this.RFPower, typeof(int));
                info.AddValue("PacketMode", this.PacketMode, typeof(byte));
                info.AddValue("FrequencyDeviation", this.FrequencyDeviation, typeof(uint));
                info.AddValue("ReceiverBandwidth", this.ReceiverBandwidth, typeof(uint));
                info.AddValue("Preamble", this.Preamble, typeof(uint));
                info.AddValue("SyncWord", this.SyncWord, typeof(uint));
                info.AddValue("AddressFiltering", this.AddressFiltering, typeof(bool));
                info.AddValue("NodeAddress", this.NodeAddress, typeof(uint));
                info.AddValue("BroadcastAddress", this.BroadcastAddress, typeof(uint));
                info.AddValue("AESEncryption", this.AESEncryption, typeof(bool));
                info.AddValue("RFCommandDelay", this.RFCommandDelay, typeof(uint));
                info.AddValue("RFCommandTimeout", this.RFCommandTimeout, typeof(uint));
                info.AddValue("ModulationType", this.ModulationType, typeof(byte));
                info.AddValue("PacketEncoding", this.PacketEncoding, typeof(bool));
                info.AddValue("CostParameters", this.CostParameters, typeof(double));
                info.AddValue("Temperature", this.Temperature, typeof(uint));

                info.AddValue("WiFiWebServerConfigurationIP", this.WiFiWebServerConfigurationIP, typeof(IPAddress));
                info.AddValue("WiFiWebServerConfigurationPort", this.WiFiWebServerConfigurationPort, typeof(uint));
                info.AddValue("WiFiServerIP", this.WiFiServerIP, typeof(IPAddress));
                info.AddValue("WiFiServerPort", this.WiFiServerPort, typeof(ushort));
                info.AddValue("WiFiClientIP", this.WiFiClientIP, typeof(IPAddress));
                info.AddValue("WiFiClientPort", this.WiFiClientPort, typeof(uint));

                info.AddValue("SerialNumber", this.SerialNumber, typeof(uint));
                info.AddValue("LCDContrast", this.LCDContrast, typeof(uint));
                info.AddValue("MeterToReadType", this.MeterToReadType, typeof(byte));
                info.AddValue("MeterToRead", this.MeterToRead, typeof(byte[]));
                info.AddValue("MeterPassword", this.MeterPassword, typeof(byte[]));
                info.AddValue("BuzzerSettings", this.BuzzerSettings, typeof(bool));
                info.AddValue("ReadHumidity", this.ReadHumidity, typeof(bool));
                info.AddValue("TempratureSettings", this.TempratureSettings, typeof(byte));
                info.AddValue("USBParams", this.USBParams, typeof(byte));

                info.AddValue("WiFiSSID", this.WiFiSSID, typeof(byte[]));
                info.AddValue("WiFiPassword", this.WiFiPassword, typeof(byte[]));

                //info.AddValue("", this., typeof());
                //info.AddValue("", this., typeof());
                //info.AddValue("", this., typeof());


            }
            catch
            {
                throw;
            }
        }

        #endregion

        public object Clone()
        {
            MemoryStream memStream = null;
            Object dp = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (memStream = new MemoryStream(256))
                {
                    formatter.Serialize(memStream, this);
                    memStream.Seek(0, SeekOrigin.Begin);
                    dp = formatter.Deserialize(memStream);
                }
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Clone Object", ex);
            }
        }
    }
}
