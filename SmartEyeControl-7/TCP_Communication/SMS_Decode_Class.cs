using System;
using System.Collections.Generic;
using System.Text;

namespace ReadSMS_AT_CS20
{
    public class SMS_Decode_class
    {
        public byte SMSC_length;
        public byte Address_type;
        public string SMSC_no;
        public byte SMS_deliver;
        public byte Sender_no_length;
        public byte Sender_no_type;
        public string Sender_no;
        public byte Protocol_Identifier;
        public byte Decoding_Scheme;
        public DateTime Time_stamp;
        public byte Data_length;
        public byte[] Data;

        public SMS_Decode_class()
        {
            byte[] Phone_no = new byte[12];
            byte[] Decode = new byte[12];
            byte[] Data = new byte[140];
        }

        public void SMS_decode(string Passed_string)
        {
            byte[] Byte_array = new byte[280];
            byte offset = 0;
            SMSC_length = decode_2_bytes_in_1(Passed_string.Substring(offset));
            offset += 2;
            Address_type = decode_2_bytes_in_1(Passed_string.Substring(offset));
            offset += 2;
            SMSC_no = Decode_phone_number(Passed_string.Substring(offset, (byte)(SMSC_length - 1) * 2), (byte)((SMSC_length - 1) * 2));
            offset += (byte)((SMSC_length - 1) * 2);
            SMS_deliver = decode_2_bytes_in_1(Passed_string.Substring(offset));
            offset += 2;

            Sender_no_length = decode_2_bytes_in_1(Passed_string.Substring(offset));
            offset += 2;
            if (Sender_no_length % 2 == 0)
                Sender_no_length = (byte)(Sender_no_length / 2);
            else Sender_no_length = (byte)(Sender_no_length / 2 + 1);
            Address_type = decode_2_bytes_in_1(Passed_string.Substring(offset));
            offset += 2;
            Sender_no = Decode_phone_number(Passed_string.Substring(offset, Sender_no_length * 2), (byte)(Sender_no_length * 2));
            offset += (byte)(Sender_no_length * 2);
            Protocol_Identifier = decode_2_bytes_in_1(Passed_string.Substring(offset));
            offset += 2;
            Decoding_Scheme = decode_2_bytes_in_1(Passed_string.Substring(offset));
            offset += 2;
            //Time_stamp.Year = decode_2_bytes_in_1(Passed_string);;
            offset += 2;
            //Time_stamp.Month = decode_2_bytes_in_1(Passed_string);
            offset += 2;
            //Time_stamp.Day = decode_2_bytes_in_1(Passed_string);
            offset += 2;
            //Time_stamp.Hour = decode_2_bytes_in_1(Passed_string);
            offset += 2;
            //Time_stamp.Minute = decode_2_bytes_in_1(Passed_string);
            offset += 2;
            //Time_stamp.Second = decode_2_bytes_in_1(Passed_string);
            offset += 2;
            //Time_stamp.Second = decode_2_bytes_in_1(Passed_string);
            offset += 2;

            Data_length = decode_2_bytes_in_1(Passed_string.Substring(offset));
            offset += 2;
            Data = Decode_HEX_array(Passed_string.Substring(offset), Data_length);


        }

        public string Decode_phone_number(string s, byte length)
        {
            int i;
            string returned = "";

            for (i = 0; i < s.Length - 1; i = i + 2)
            {
                returned += decode_2_bytes_in_2(s.Substring(i));
            }
            return returned;
        }

        public byte[] Decode_HEX_array(string s, int size)
        {
            byte[] Byte_array = new byte[size];
            int i;
            byte b;
            for (i = 0; i < s.Length; i = i + 2)
            {
                Byte_array[i / 2] = decode_2_bytes_in_1(s.Substring(i));

            }
            return Byte_array;
        }

        public byte decode_a_byte(char c)
        {
            byte b = Convert.ToByte(c);

            byte upper_nibble = (byte)(b / 16);
            byte lower_nibble = (byte)(b % 16);
            upper_nibble = decode_a_HEX(Convert.ToChar(upper_nibble));
            lower_nibble = decode_a_HEX(Convert.ToChar(lower_nibble));
            b = (byte)((lower_nibble * 16) + upper_nibble);
            return b;

        }

        public byte decode_2_bytes_in_1(string s)  //ascii combine to fora
        {
            // **** RESUME
            byte b, c;
            b = decode_a_HEX(Convert.ToChar(s.Substring(0, 1)));
            c = decode_a_HEX(Convert.ToChar(s.Substring(1, 1)));
            byte returned = (byte)(b * 16 + c);
            return returned;

        }

        public string decode_2_bytes_in_2(string s) // acsii flip method
        {
            // **** RESUME
            byte b, c;
            b = decode_a_HEX(Convert.ToChar(s.Substring(0, 1)));
            c = decode_a_HEX(Convert.ToChar(s.Substring(1, 1)));
            string returned = c.ToString() + b.ToString();
            return returned;

        }

        public byte decode_a_HEX(char c)
        {
            byte returned = 0;
            switch (c)
            {
                case '0':
                    returned = 0;
                    break;
                case '1':
                    returned = 1;
                    break;
                case '2':
                    returned = 2;
                    break;
                case '3':
                    returned = 3;
                    break;
                case '4':
                    returned = 4;
                    break;
                case '5':
                    returned = 5;
                    break;
                case '6':
                    returned = 6;
                    break;
                case '7':
                    returned = 7;
                    break;
                case '8':
                    returned = 8;
                    break;
                case '9':
                    returned = 9;
                    break;
                case 'A':
                    returned = 10;
                    break;
                case 'B':
                    returned = 11;
                    break;
                case 'C':
                    returned = 12;
                    break;
                case 'D':
                    returned = 13;
                    break;
                case 'E':
                    returned = 14;
                    break;
                case 'F':
                    returned = 15;
                    break;
                default:
                    returned = 00;
                    break;
            }
            return returned;
        }


    }
}
