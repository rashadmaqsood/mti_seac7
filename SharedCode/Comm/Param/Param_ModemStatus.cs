using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;
using System.Xml.Serialization;

namespace SharedCode.Comm.Param
{
    [XmlInclude(typeof(Param_ModemStatus))]
    [Serializable]
    public class Param_ModemStatus : ICustomStructure
    {
        public short    SignalStrength      {get;set;}
        public bool     SimInsertedStatus   { get; set; }
        public bool     PinStatus           { get; set; }
        public bool     NetworkRegStatus    { get; set; }
        public bool     GPRS_AttachmentStatus { get; set; }
        public string   FirmwareVersion     { get; set; }
        public string   MouduleInfo         { get; set; }
        public string   IMEI                { get; set; }
        public string   IMSI                { get; set; }
        public string   NetworkCode         { get; set; }
        public string   ServerIP            { get; set; }
        public ushort   PortN               { get; set; }
        public string   AssingnedIP         { get; set; }
        public string   CellID              { get; set; }
        public string   LAC                 { get; set; }
        /// public string DisplayWindow { get; set; }

        public void Decode_Data(byte[] buffer)
        {
            int offset=0;
            Decode_Data(buffer, ref offset, buffer.Length);
        }
        public void Decode_Data(byte[] buffer, ref int offset,int length)
        {
            if (buffer == null || offset + length > buffer.Length || length < 96) throw new Exception("Invalid Data Buffer for Modem Status.");
            int index = offset;
            SignalStrength = BitConverter.ToInt16(buffer, index); index += 2;
            SimInsertedStatus=Convert.ToBoolean(buffer[index++]);
            PinStatus = Convert.ToBoolean(buffer[index++]);
            NetworkRegStatus = Convert.ToBoolean(buffer[index++]);
            GPRS_AttachmentStatus = Convert.ToBoolean(buffer[index++]);

            FirmwareVersion = Encoding.ASCII.GetString(buffer, index, 8).Replace("\0", string.Empty); index += 8;
            MouduleInfo = Encoding.ASCII.GetString(buffer, index, 12).Replace("\0",string.Empty); index += 12;
            IMEI = Encoding.ASCII.GetString(buffer, index, 15).Replace("\0", string.Empty); index += 15;
            IMSI = Encoding.ASCII.GetString(buffer, index, 15).Replace("\0", string.Empty); index += 15;
            NetworkCode = Encoding.ASCII.GetString(buffer, index, 2).Replace("\0", string.Empty); index += 2;
            ServerIP = buffer[index++] + "." + buffer[index++] + "." + buffer[index++] + "." + buffer[index++];
            PortN = BitConverter.ToUInt16(buffer, index); index += 2;
            AssingnedIP = buffer[index++] + "." + buffer[index++] + "." + buffer[index++] + "." + buffer[index++];
            CellID = Encoding.ASCII.GetString(buffer, index, 8).Replace("\0", string.Empty); index += 8;
            LAC = Encoding.ASCII.GetString(buffer, index, 8).Replace("\0", string.Empty); index += 8;
            /// DisplayWindow = string.Empty;
        }

        object ICloneable.Clone()
        {
            return new Param_ModemStatus();
        }

         public byte[] Encode_Data()
          {
              return new byte[0];
          }
        public override string ToString()
        {
            return
             "SignalStrength::" + SignalStrength+" dbm" +Environment.NewLine+
             "SimInsertedStatus::" + (SimInsertedStatus?"Inserted":"Not Inserted") + Environment.NewLine +
             "PinStatus::" + (PinStatus?"Disable":"Enable") + Environment.NewLine +
             "NetworkRegStatus::" + (NetworkRegStatus?"Registered":"Not Registered") + Environment.NewLine +
             "GPRS_AttachmentStatus::" + (GPRS_AttachmentStatus?"Available":"Not Available") + Environment.NewLine +
            "FirmwareVersion::" + FirmwareVersion + Environment.NewLine +
            "MouduleInfo::" + MouduleInfo + Environment.NewLine +
            "IMEI::" + IMEI + Environment.NewLine +
            "IMSI::" + IMSI + Environment.NewLine +
            "NetworkCode::" + NetworkCode + Environment.NewLine +
            "ServerIP::" + ServerIP + Environment.NewLine +
            "PortN::" + PortN + Environment.NewLine +
            "AssingnedIP::" + AssingnedIP + Environment.NewLine +
            "CellID::" + CellID + Environment.NewLine +
            "LAC::" + LAC+ Environment.NewLine;
        }

    }

}
