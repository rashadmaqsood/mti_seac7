using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ReadSMS_AT_CS20
{
    public class SMSWakeUP
    {
        private GSMModemHandler _modemHandler;
        private const int Retries = 15;

        public GSMModemHandler ModemHandler
        {
            get { return _modemHandler; }
        }

        public SMSWakeUP()
        {
            _modemHandler = new GSMModemHandler();
        }

        public bool SendWakeUpSMS(SMS_Params Param_sms,String phoneNumber)
        {
            try
            {
                ModemHandler.InitModem(3);
                ModemHandler.ExecCommand("ATE0", 100, "Not sent");
                ModemHandler.ExecCommand("AT+CMGF = 0", 250, "Error,SET SMS PDU Mode");
                byte[] encoded_string = Param_sms.EncodePacket();
                byte[] data = encoded_string;
                int actual_length = (phoneNumber.Length / 2) + 1 + data.Length + 5;
                string output;
                output = SubmitPdu.GetPdu(phoneNumber, data, "", out actual_length);
                string s1 = "AT+CMGS=" + actual_length.ToString() + '\x0D';
                string out_to_modem = output + '\x1A';
                ModemHandler.ExecCommand(s1, "Send SMS Command Error");
                String cmdResponse = ModemHandler.ExecCommand(out_to_modem, 4000, "Send SMS Command Error");
                if (!String.IsNullOrEmpty(cmdResponse) && cmdResponse.EndsWith("\r\nOK\r\n") && cmdResponse.Contains("+CMGS:"))
                {
                   
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Wake SMS command is not sent", ex);
            }
            finally 
            {
                ModemHandler.Dispose();
            }
        }

        public bool SendWakeUpVoiceCall(String phoneNumber, int retries)
        {
            try
            {
                int retries_make = 0;
                bool continue_to_send = false;
                bool dialling_ok = false;
                ModemHandler.InitModem(5);
                ModemHandler.ExecCommand("ATE0", 100, "ATE0, Not sent");
                ModemHandler.ExecCommand("AT+COLP=1", 1000, "AT+COLP=1, Not sent");
                ModemHandler.ExecCommand("ATD" + phoneNumber +";", "Wake Up Voice Call Command Not Sent");
                string s = ModemHandler.ExecCommand("AT+CLCC", 1000, "AT+CLCC Not sent");
                do
                {
                    if (retries_make < retries)
                    {
                        //string debug = ModemHandler.ExecCommand("ATD" + phoneNumber + ";", "Wake Up Voice Call Command Not Sent");
                    }
                    else return false; 
                    Thread.Sleep(1500);
                    s = ModemHandler.ExecCommand("AT+CLCC", 1000, "AT+CLCC Not sent");
                    if (s.Equals("\r\nOK\r\n"))     // modem is not dialing so this block will retry
                    {
                        retries_make++;
                        
                    }
                    else//modem is dialing or already connected check the status
                    {
                        string s1 = s.Substring(13, 1);
                        string s2 = s.Substring(20,9);
                        string s3 = phoneNumber.Substring(0,9);
                        if (s1.Equals("0") || s1.Equals("1") || s1.Equals("3") && string.Equals(s2,s3))
                        {
                            dialling_ok = true;
                            return true;
                        }
                        else
                        {
                            dialling_ok = false;
                            retries_make++;
                        }
                    }

                }
                while (!dialling_ok);
                return false;
                
            }
            catch (Exception ex)
            {
                throw new Exception("Wake Voice Call is not sent", ex);
            }
            finally
            {
                ModemHandler.Dispose();
            }
        }

        public bool SendWakeUpVoiceCall(String phoneNumber)
        {
            return SendWakeUpVoiceCall(phoneNumber,Retries);
        }
    }
}
