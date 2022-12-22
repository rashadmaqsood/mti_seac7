using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml.Serialization;
using SmartDebugUtility.Common;
using SmartEyeControl_7.Common;

namespace ReadSMS_AT_CS20
{
    public class SMS_Params
    {
        public bool Password_FLAG0;
        public bool HDLC_TCP_FLAG1;
        public bool TCP_UDP_FLAG2;
        public bool IP_and_Port_FLAG3;
        public bool Use_Backup_IP_FLAG4;
        public string Password;
        public ushort IP_Port;
        [XmlIgnore()]        
        public IPAddress IP;
        public byte HDLC_TCP;
        public byte TCP_UDP; 
        /// <summary>
        /// Supporting Variables
        /// </summary>
        [XmlIgnore()]
        public ushort FLAGs;
        [XmlIgnore()]
        public char Header = '%';
        [XmlIgnore()]
        public ushort Flag_Encoded;

        [XmlArray("Raw_IP")]
        public byte[] _IP_
        {
            get 
            {
                if (IP == null)
                    return IPAddress.Loopback.GetAddressBytes();
                else
                    return IP.GetAddressBytes();
            }
            set
            {
                IP = new IPAddress(value);
            }
        }

        public byte[] EncodePacket()
        {
            int SMS_8bit_encoding_of_Data_lenth = 1;
            byte[] Encoded_byte_array = new byte[SMS_8bit_encoding_of_Data_lenth];
            int offset = 0;
            Encoded_byte_array[offset++] = (byte)'%';
            Encoded_byte_array = App_Common.Append_to_End(Encoded_byte_array, EncodeFlags());
            Encoded_byte_array = App_Common.Append_to_End(Encoded_byte_array, EncodePassword());
            Encoded_byte_array = App_Common.Append_to_End(Encoded_byte_array, HDLC_TCP);
            Encoded_byte_array = App_Common.Append_to_End(Encoded_byte_array, TCP_UDP);
            Encoded_byte_array = App_Common.Append_to_End(Encoded_byte_array, EncodeIP(IP));
            Encoded_byte_array = App_Common.Append_to_End(Encoded_byte_array, EncodeIP_Port(IP_Port));
            return Encoded_byte_array;
        }

        public byte[] EncodeFlags()
        {
            FLAGs = 0;
            if (Password_FLAG0)
                FLAGs += 1;
            if (HDLC_TCP_FLAG1)
                FLAGs += 2;
            if (TCP_UDP_FLAG2)
                FLAGs += 4;
            if (IP_and_Port_FLAG3)
                FLAGs += 8;
            byte[] return_byte_array = new byte[2];
            return_byte_array[0] = 0;
            return_byte_array[1] = (byte)FLAGs;

            return return_byte_array;

        }

        public byte[] EncodePassword()
        {
            int password_lenght = 20;
            byte[] modified_password = new byte[password_lenght];
            for (int i = 0; i < Password.Length; i++)
            {
                modified_password[i] = (byte)Convert.ToChar(Password.Substring(i, 1));
            }
            return modified_password;
        }

        public byte[] EncodeIP(IPAddress IP)
        {
            byte[] bytes = IP.GetAddressBytes();
            //ulong modified_ip;
            //modified_ip =  (16777216 * (ulong)bytes[0] + 65536 * (ulong)bytes[1] +256 * (ulong)bytes[2] +(ulong)bytes[3] );
            string s = "";
            bytes[0] = (byte)bytes[0];
            bytes[1] = (byte)bytes[1];
            bytes[2] = (byte)bytes[2];
            bytes[3] = (byte)bytes[3];

            return bytes;
        }

        public byte[] EncodeIP_Port(ushort Port)
        {

            byte a = (byte)(Port / 256);
            byte b = (byte)(Port % 256);
            byte[] bytes = new byte[2];
            //ulong modified_ip;
            //modified_ip =  (16777216 * (ulong)bytes[0] + 65536 * (ulong)bytes[1] +256 * (ulong)bytes[2] +(ulong)bytes[3] );
            string s = "";
            bytes[0] = (byte)a;
            bytes[1] = (byte)b;
            return bytes;
        }

       

    }
}
