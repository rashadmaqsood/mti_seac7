using System;
using System.Linq;
using System.IO.Ports;
using Microsoft.Win32;
namespace SharedCode.Comm.HelperClasses
{
    public class Dongle
    {
        #region DATA MEMBERS
        private SerialPort objComPort;
        private readonly string regKey = "\\BIOS_VGA\\ADDRESS_RANGES";
        private readonly string regEMSCounter = "0x3FF29";
        private readonly string regDongleCounter = "0x3FF89";
        private string keyName = "";
        private string subKey1 = "";
        private string subKey2 = "";
        private stInt convertType;

        private byte[] inputBuffer;
        private byte[] outputBuffer;
        private byte[] commBuffer;

        private byte[] serialBuffer;
        public byte[] commandBuffer;
        public byte[] counterBuffer;
        private byte[] recDataBuffer;
        private byte[] recBuffer;

        public uint localEMSCTR;
        public uint localDongleCTR;

        public uint remoteDongleCTR;
        public uint remoteCMD;
        string[] HDDSerials;

        private uint bufferCTR;
        public readonly static byte[][] EncryptTable;


        //---------------------------------
        //  0--> No Command 
        //  1--> Login Command(Dongle Present)
        //  2--> Data Command
        //
        //---------------------------------
        #endregion
        //============================Constructor=================================================
        static Dongle()
        {
            EncryptTable = new byte[64][];
            #region Encryption Table
            EncryptTable[0] = new byte[16] { 0x3F, 0x5D, 0xB8, 0xEF, 0x20, 0x03, 0x31, 0x8E, 0x6F, 0xCA, 0xEE, 0x55, 0x5E, 0x79, 0xB4, 0xC9 };
            EncryptTable[1] = new byte[16] { 0xE7, 0x68, 0x88, 0xB2, 0x4B, 0x2A, 0xBE, 0xD6, 0xBD, 0x6F, 0x5A, 0x09, 0x5F, 0x28, 0x95, 0x62 };
            EncryptTable[2] = new byte[16] { 0x6D, 0xEB, 0xBE, 0xCE, 0xB8, 0x9A, 0x44, 0xCB, 0x57, 0x40, 0x10, 0x6E, 0x99, 0x3B, 0x18, 0xA6 };
            EncryptTable[3] = new byte[16] { 0x3E, 0xA0, 0x31, 0xC5, 0xE5, 0xDB, 0x25, 0x3C, 0xC9, 0x96, 0x05, 0xA6, 0xA9, 0xDC, 0xC0, 0x09 };
            EncryptTable[4] = new byte[16] { 0x05, 0x7F, 0xF4, 0x58, 0x8F, 0xB6, 0x06, 0x3C, 0xE1, 0x0C, 0x6E, 0x14, 0x6C, 0x75, 0x54, 0x3A };
            EncryptTable[5] = new byte[16] { 0xB0, 0xC0, 0x5F, 0x87, 0xB3, 0x36, 0xCC, 0x1A, 0xAC, 0x78, 0xBF, 0x56, 0xFE, 0xAD, 0xD7, 0x2A };
            EncryptTable[6] = new byte[16] { 0xA2, 0xDE, 0x06, 0x95, 0x8D, 0xA1, 0x9E, 0x69, 0x76, 0xF5, 0xAE, 0x4F, 0xBF, 0x6E, 0x90, 0x0B };
            EncryptTable[7] = new byte[16] { 0xA9, 0x92, 0xBB, 0x02, 0x9A, 0x82, 0xDF, 0xF8, 0xCD, 0xDD, 0x31, 0x1F, 0x49, 0xE1, 0x03, 0x4E };
            EncryptTable[8] = new byte[16] { 0x10, 0xD3, 0x97, 0x8D, 0x98, 0x6E, 0x35, 0xDA, 0x7F, 0xC7, 0x7B, 0x27, 0x7A, 0x6F, 0xF4, 0xA2 };
            EncryptTable[9] = new byte[16] { 0x8E, 0xDC, 0xED, 0x3A, 0x84, 0x53, 0x85, 0x5F, 0x96, 0x8D, 0x03, 0x09, 0x6F, 0xC1, 0x6B, 0xFB };
            EncryptTable[10] = new byte[16] { 0x09, 0x26, 0x53, 0x49, 0x99, 0x01, 0xF2, 0x18, 0x62, 0x47, 0x7C, 0xA5, 0x85, 0xC0, 0xAA, 0x88 };
            EncryptTable[11] = new byte[16] { 0xC9, 0x67, 0x9D, 0x3B, 0x57, 0x11, 0xE4, 0xD5, 0x6F, 0x4F, 0xDB, 0x1D, 0x59, 0x95, 0x38, 0xBB };
            EncryptTable[12] = new byte[16] { 0x9E, 0x9C, 0xE0, 0x15, 0x79, 0x05, 0xFE, 0xAA, 0x89, 0x3E, 0x57, 0xD0, 0xC8, 0xA9, 0xD8, 0x45 };
            EncryptTable[13] = new byte[16] { 0xC0, 0xFB, 0x72, 0x0B, 0xFC, 0xE3, 0x27, 0xE4, 0xBD, 0xEC, 0x64, 0x61, 0xEF, 0xA5, 0x91, 0x16 };
            EncryptTable[14] = new byte[16] { 0x5C, 0xFE, 0xE6, 0x2B, 0x1F, 0xF6, 0x82, 0x18, 0x5A, 0x74, 0xB6, 0xB0, 0x2C, 0x72, 0xA7, 0x60 };
            EncryptTable[15] = new byte[16] { 0xDD, 0x60, 0xD0, 0xB1, 0x5C, 0xC7, 0x74, 0x15, 0xEB, 0x2D, 0x44, 0xDF, 0x1A, 0x39, 0x9F, 0x93 };
            EncryptTable[16] = new byte[16] { 0xF1, 0x17, 0x10, 0xE1, 0x72, 0x1E, 0xA3, 0xEA, 0x1D, 0xB0, 0x42, 0x4E, 0x97, 0x62, 0x3F, 0x60 };
            EncryptTable[17] = new byte[16] { 0x87, 0x5D, 0x2D, 0x38, 0x5E, 0x05, 0xF3, 0xEC, 0x61, 0xD7, 0x24, 0x9E, 0xC0, 0x98, 0x8A, 0xB9 };
            EncryptTable[18] = new byte[16] { 0xC9, 0xAC, 0x01, 0x79, 0x5C, 0xC3, 0x8B, 0xA9, 0x9F, 0xBB, 0xA0, 0xB1, 0xF2, 0xC2, 0xC6, 0xCF };
            EncryptTable[19] = new byte[16] { 0x26, 0xBB, 0x62, 0xA5, 0xE9, 0xE3, 0xCD, 0xF4, 0x85, 0xB5, 0xAB, 0xA7, 0xCA, 0x0B, 0x79, 0x14 };
            EncryptTable[20] = new byte[16] { 0x4A, 0x86, 0x64, 0x7D, 0xC3, 0x2F, 0x61, 0xDC, 0x60, 0x5E, 0x7A, 0xE1, 0x26, 0xDA, 0x66, 0x35 };
            EncryptTable[21] = new byte[16] { 0x22, 0x43, 0x5D, 0x02, 0xE7, 0xAD, 0x2B, 0xB4, 0xC0, 0x8E, 0x82, 0x01, 0x21, 0xDA, 0x94, 0x27 };
            EncryptTable[22] = new byte[16] { 0xDA, 0x6C, 0xE1, 0x75, 0x91, 0xA8, 0x4E, 0x0D, 0x6F, 0x5F, 0x78, 0xE7, 0x1A, 0xF2, 0x47, 0x19 };
            EncryptTable[23] = new byte[16] { 0xE2, 0xBA, 0xC6, 0x56, 0x3F, 0xA8, 0x32, 0xB5, 0x7B, 0x29, 0x51, 0xB4, 0xAB, 0x4D, 0x05, 0x7D };
            EncryptTable[24] = new byte[16] { 0xE5, 0x28, 0x20, 0x68, 0xAD, 0x77, 0x7A, 0xC0, 0xB0, 0x86, 0x41, 0xCA, 0xB5, 0x52, 0x91, 0x04 };
            EncryptTable[25] = new byte[16] { 0xD0, 0xEB, 0x44, 0xAA, 0xD8, 0x3F, 0x0C, 0x7E, 0x1D, 0x50, 0xBE, 0xC9, 0x52, 0xAB, 0xF1, 0x9D };
            EncryptTable[26] = new byte[16] { 0xD0, 0x3B, 0xC8, 0x5E, 0xFF, 0xE3, 0x0C, 0x80, 0x0D, 0x9D, 0x7E, 0x92, 0xE0, 0x42, 0x6B, 0x7C };
            EncryptTable[27] = new byte[16] { 0x53, 0xC5, 0x80, 0x05, 0x9D, 0x54, 0xE0, 0x97, 0x0D, 0xC8, 0x74, 0x47, 0xFC, 0x3E, 0x82, 0x10 };
            EncryptTable[28] = new byte[16] { 0x05, 0x3D, 0x82, 0x5F, 0x6F, 0x37, 0x2D, 0xD5, 0xEB, 0x6A, 0xD6, 0x48, 0x84, 0x09, 0xFD, 0x0B };
            EncryptTable[29] = new byte[16] { 0xD3, 0x97, 0x22, 0x6F, 0x73, 0xF4, 0xD7, 0x89, 0xB4, 0x5C, 0x19, 0x36, 0x94, 0x4C, 0xE0, 0x5D };
            EncryptTable[30] = new byte[16] { 0xEA, 0x27, 0xF4, 0x74, 0xE5, 0xB7, 0x04, 0x46, 0xB5, 0xB6, 0xF1, 0xF2, 0x88, 0xF0, 0x70, 0x38 };
            EncryptTable[31] = new byte[16] { 0xB8, 0xA2, 0xD0, 0xF0, 0x43, 0x27, 0x19, 0xDB, 0x7B, 0xD2, 0x56, 0x9F, 0xFE, 0x1E, 0x33, 0x0D };
            EncryptTable[32] = new byte[16] { 0xE9, 0x05, 0xC9, 0xA4, 0x49, 0xAD, 0xB9, 0x5B, 0xD2, 0x4A, 0x79, 0x9B, 0xD4, 0x3F, 0xEC, 0x8B };
            EncryptTable[33] = new byte[16] { 0x6B, 0x86, 0x35, 0x91, 0x94, 0x53, 0xCC, 0x16, 0xC8, 0xF4, 0xD2, 0x89, 0x26, 0xFB, 0xA1, 0xA6 };
            EncryptTable[34] = new byte[16] { 0x69, 0xA0, 0xA8, 0xF7, 0x83, 0x61, 0x76, 0x9C, 0xAB, 0xEB, 0x15, 0x49, 0x50, 0x3E, 0x98, 0x8D };
            EncryptTable[35] = new byte[16] { 0x52, 0x0C, 0xF7, 0x59, 0x71, 0x61, 0x1B, 0xC0, 0x06, 0x88, 0x36, 0xFB, 0xF0, 0x2E, 0x55, 0xB1 };
            EncryptTable[36] = new byte[16] { 0xD1, 0xC0, 0x38, 0x76, 0x7B, 0x1B, 0x61, 0x92, 0xA6, 0x64, 0x6B, 0x04, 0xE3, 0x35, 0x9D, 0xC5 };
            EncryptTable[37] = new byte[16] { 0xD6, 0xF8, 0xBF, 0x51, 0x9F, 0x98, 0x2D, 0x63, 0x99, 0x58, 0x29, 0x01, 0x48, 0xFA, 0x75, 0xB8 };
            EncryptTable[38] = new byte[16] { 0x8B, 0x2E, 0x22, 0x29, 0x1A, 0x2A, 0xA2, 0xC4, 0x2D, 0x7C, 0x25, 0xD3, 0x78, 0x6B, 0x23, 0xBB };
            EncryptTable[39] = new byte[16] { 0x5F, 0x18, 0x35, 0x7F, 0x68, 0x42, 0x28, 0x86, 0xEC, 0x23, 0x54, 0x9E, 0x13, 0xAC, 0x2A, 0x41 };
            EncryptTable[40] = new byte[16] { 0xFE, 0xAF, 0x0D, 0x16, 0x46, 0xC0, 0x63, 0xBA, 0xA6, 0xF9, 0xEA, 0xC1, 0xF4, 0x29, 0x51, 0xF8 };
            EncryptTable[41] = new byte[16] { 0x56, 0x2F, 0xFF, 0xED, 0xB2, 0xA5, 0x37, 0xB1, 0x66, 0xC6, 0x5E, 0xDD, 0x3A, 0x89, 0x9B, 0xD3 };
            EncryptTable[42] = new byte[16] { 0x92, 0x0E, 0xA1, 0x47, 0xE8, 0x3C, 0xC9, 0xFD, 0x7A, 0xA7, 0x64, 0xD3, 0x40, 0xB7, 0x4F, 0x04 };
            EncryptTable[43] = new byte[16] { 0x22, 0x07, 0xC7, 0xA3, 0x67, 0x0B, 0x7F, 0x6E, 0x6F, 0xF5, 0x12, 0xC5, 0xA5, 0xDA, 0xF0, 0xF8 };
            EncryptTable[44] = new byte[16] { 0xAF, 0x12, 0x87, 0xC2, 0xE9, 0xDC, 0xFE, 0xA4, 0xF0, 0x4B, 0x39, 0x14, 0x45, 0x5D, 0x46, 0x64 };
            EncryptTable[45] = new byte[16] { 0x2A, 0x67, 0x36, 0xA7, 0x6D, 0xB9, 0x2B, 0x42, 0x6F, 0x80, 0xB2, 0x5F, 0x3C, 0xE7, 0x53, 0x38 };
            EncryptTable[46] = new byte[16] { 0xBD, 0x82, 0x67, 0x92, 0x30, 0xEB, 0x2A, 0x87, 0xD4, 0xAD, 0x11, 0x89, 0xE8, 0x62, 0x5E, 0xA4 };
            EncryptTable[47] = new byte[16] { 0xD7, 0x19, 0xF0, 0x04, 0xAD, 0xFA, 0x61, 0xB6, 0xCE, 0x2D, 0x4B, 0xB2, 0xE7, 0xF7, 0xEA, 0x1A };
            EncryptTable[48] = new byte[16] { 0x25, 0x27, 0xE8, 0xBC, 0xA4, 0xB0, 0x74, 0xDF, 0x2A, 0x97, 0x95, 0x3C, 0x15, 0x10, 0xBD, 0x4A };
            EncryptTable[49] = new byte[16] { 0x92, 0xE3, 0xA2, 0xBE, 0x11, 0x16, 0x4A, 0x53, 0xF4, 0xC5, 0x64, 0xC6, 0x8F, 0x54, 0xDD, 0x26 };
            EncryptTable[50] = new byte[16] { 0x4D, 0xC8, 0xB3, 0x4B, 0x2F, 0x73, 0x06, 0xA3, 0x7B, 0xCF, 0x6D, 0x34, 0xB1, 0xAD, 0x8E, 0xDE };
            EncryptTable[51] = new byte[16] { 0xC2, 0x8F, 0xF0, 0xE1, 0x7D, 0x52, 0x0D, 0xA0, 0x4A, 0x10, 0x14, 0xA4, 0x1B, 0x44, 0x56, 0xE4 };
            EncryptTable[52] = new byte[16] { 0x9E, 0x51, 0x70, 0x44, 0xB7, 0x7B, 0x05, 0x5B, 0x2F, 0x1F, 0x40, 0x79, 0xA6, 0x82, 0xF7, 0xE8 };
            EncryptTable[53] = new byte[16] { 0xCE, 0xE2, 0x85, 0x73, 0xDB, 0xF7, 0xD2, 0x26, 0x36, 0xD5, 0xB4, 0x54, 0x72, 0x10, 0x7A, 0xDD };
            EncryptTable[54] = new byte[16] { 0x80, 0x23, 0xC5, 0xB0, 0x27, 0x12, 0x9A, 0x90, 0xAD, 0x4D, 0xB6, 0x14, 0xDA, 0xD7, 0x22, 0xF1 };
            EncryptTable[55] = new byte[16] { 0x21, 0xA8, 0x07, 0x7C, 0x15, 0x50, 0xC2, 0x6B, 0x22, 0xDD, 0x3C, 0xDB, 0x7D, 0x00, 0x73, 0x98 };
            EncryptTable[56] = new byte[16] { 0x5C, 0x6B, 0x5D, 0x98, 0x63, 0x7D, 0xEF, 0xC8, 0x5F, 0x22, 0x78, 0x0C, 0x37, 0xF3, 0x34, 0x81 };
            EncryptTable[57] = new byte[16] { 0x20, 0xA5, 0x1E, 0x05, 0x10, 0xA2, 0x06, 0xF9, 0x74, 0xF0, 0xE2, 0x46, 0x25, 0x5B, 0x69, 0x9D };
            EncryptTable[58] = new byte[16] { 0x98, 0xD0, 0xDE, 0x04, 0x56, 0x07, 0x2A, 0x8E, 0xAC, 0x65, 0x2E, 0x69, 0xA4, 0x20, 0x57, 0x1F };
            EncryptTable[59] = new byte[16] { 0x34, 0xA4, 0x73, 0x15, 0xB4, 0x36, 0xC2, 0x58, 0x95, 0xD6, 0x2F, 0x98, 0x51, 0x6B, 0x83, 0x75 };
            EncryptTable[60] = new byte[16] { 0x9E, 0x1A, 0xF0, 0xF9, 0xE6, 0xF6, 0x74, 0x67, 0xFC, 0xDF, 0x7F, 0x34, 0x09, 0xA4, 0xB2, 0x52 };
            EncryptTable[61] = new byte[16] { 0xC4, 0x6B, 0xAD, 0xB4, 0xEB, 0x53, 0x23, 0x0E, 0xED, 0x58, 0x2F, 0xDC, 0xE8, 0x76, 0xEA, 0xA7 };
            EncryptTable[62] = new byte[16] { 0xD4, 0x11, 0x3D, 0x84, 0xFD, 0x94, 0xF4, 0xDD, 0xB7, 0x59, 0x15, 0x74, 0x4D, 0xC8, 0x6F, 0xA4 };
            EncryptTable[63] = new byte[16] { 0x9D, 0xC3, 0x75, 0xEA, 0x9C, 0x43, 0x4D, 0xA5, 0xE5, 0x3B, 0x25, 0x1B, 0xD4, 0x80, 0xC5, 0xBB };
            #endregion
        }
        public Dongle()
        {
            objComPort = new SerialPort();
            keyName = "HKEY_CURRENT_USER" + regKey;
            subKey1 = "BIOS_VGA";
            subKey2 = "ADDRESS_RANGES";
            inputBuffer = new byte[100];
            outputBuffer = new byte[150];
            commBuffer = new byte[150];

            serialBuffer = new byte[27];
            commandBuffer = new byte[5];
            counterBuffer = new byte[7];
            recDataBuffer = new byte[100];
            recBuffer = new byte[100];


            HDDSerials = new string[20];
            HDDSerials[0] = "Y21KQNFC";         //Salman HardDriveSerial
            HDDSerials[1] = "S00JJ20X227946";   //Khurram Desktop HardDriveSerial
            HDDSerials[2] = "SB2204SGEAKKEE";   //Khurram Laptop HardDriveSerial //4JX141QB
            HDDSerials[3] = "3KC27DBZ";         //Waqas HardDriveSerial
            HDDSerials[4] = "5LR211KZ";         //Azeem HardDriveSerial
            HDDSerials[5] = "4JX141QB";         //Farhan Desktop HardDriveSerial  //SB2241SGD3TN2E
            HDDSerials[6] = "SB2241SGD3TN2E";   //Rashid Mian HardDriveSerial
            HDDSerials[7] = "JPB430HA05LEVD";   //Awais HardDriveSerial
            HDDSerials[8] = "MRG31YKKH5ELVH";   //Muzammil HardDriveSerial
            HDDSerials[9] = "S00JJ20X227856";   //Hamid HardDriveSerial
            HDDSerials[10] = "4JV674EZ";        //Ijaz HardDriveSerial
            HDDSerials[11] = "4LR0AFS3";        //Asif HardDriveSerial
            HDDSerials[12] = "Y25RLY0C";        //Panel Room HardDriveSerial
            HDDSerials[13] = "9VMM8DRG";        //Tanveer HardDriveSerial
            HDDSerials[14] = "JPB430HA06ET4D"; //Awais HardDriveSerial
            HDDSerials[15] = ""; //Awais HardDriveSerial
            HDDSerials[16] = ""; //Awais HardDriveSerial
            HDDSerials[17] = ""; //Awais HardDriveSerial
            HDDSerials[18] = ""; //Awais HardDriveSerial
            HDDSerials[19] = ""; //Awais HardDriveSerial


        }
        //========================================================================================
        public bool validateHDDSerial(string hdSerial)
        {
            for (int i = 0; i < HDDSerials.Length; i++)
            {
                if (hdSerial == HDDSerials[i])
                    return true;
            }
            return false;
        }
        //========================================================================================
        public string[] getComPortsList()
        {
            return SerialPort.GetPortNames();
        }
        //========================================================================================
        public int dongleStatus(byte[] serialNo, string portNo)
        {
            //-----------------------------------//
            //      return value Table           //
            //-----------------------------------//
            //      0-->Dongle Not Present       //    
            //      1-->Dongle OK                // 
            //      2-->Registry Error           //   
            //      3-->Communication Error      //
            //-----------------------------------//


            bool res;
            byte functionReturn;

            res = setPort(portNo);
            if (!res)
            {
                return 3;
            }
            functionReturn = dongleStatusSub(serialNo, portNo);

            res = closePort();

            return (functionReturn);

        }
        //========================================================================================
        public byte dongleStatusSub(byte[] serialNo, string portNo)
        {
            /*          ________________________________________________________________________________   
                        ||                                                                            ||
                        ||                    RETURN VALUE DESCRIPTION TABLE                          ||
                        ||____________________________________________________________________________||
                        ||                                                                            ||
                        ||      0-->                                                                  ||
                        ||      1--> command 7 and remote cnt > local cnt.                            ||
                        ||      2--> command 7 , command 7 and remote cnt == local cnt                ||
                        ||      3--> command 7 , command 7 and remote cnt !> local cnt                ||
                        ||      4--> command 7 , command != 7                                         ||
                        ||      5--> command 7 communication error                                    ||
                        ||      6--> command 8 , command 7   remote cnt > local cnt                   ||
                        ||      7--> command 8 , command 7 , command 7 remote cnt > local cnt         ||
                        ||      8--> command 8 , command 7 , command 7remote cnt !> local cnt         ||
                        ||      9--> command 8 , command 7 , command != 7                             ||
                        ||     10--> command 8 , command 7 communication error                        ||
                        ||     11--> command 8 command = 6 error.                                     ||
                        ||     12--> command 8 command != 7,6.                                        ||
                        ||     13--> command 8 communication error.                                   ||
                        ||     14--> command 4 OK.                                                    ||
                        ||     15--> command 6 error                                                  ||
                        ||     16--> command != 4,6,7,8....                                           ||
                        ||     17--> First Communication error                                        ||
                        ||     18--> Read Registry Error                                              ||
                        ||     19-->                                                                  ||
                        ||     20-->                                                                  ||
                        ||____________________________________________________________________________||    
                        ________________________________________________________________________________
 
            */
            bool res;
            byte ComRes;
            res = readRegistry();
            if (res)
            {
                ComRes = CommunicationDongle(serialNo, true);
                if (ComRes == 1)
                {
                    if (remoteCMD == 7)
                    {
                        if (remoteDongleCTR > localDongleCTR)
                        {
                            localDongleCTR = remoteDongleCTR;
                            localEMSCTR++;
                            upDateRegistry();
                            return 1;   // command 7 and remote cnt > local cnt
                        }
                        else
                        {
                            localDongleCTR = remoteDongleCTR;
                            localEMSCTR++;
                            ComRes = CommunicationDongle(serialNo, true);
                            if (ComRes == 1)
                            {
                                if (remoteCMD == 7)
                                {
                                    if (remoteDongleCTR > localDongleCTR)
                                    {
                                        localDongleCTR = remoteDongleCTR;
                                        localEMSCTR++;
                                        upDateRegistry();
                                        return 2;   // command 7 , command 7 and remote cnt == local cnt
                                    }
                                    else
                                    {
                                        return 3; // command 7 , command 7 and remote cnt !> local cnt
                                    }
                                }
                                else
                                {
                                    return 4; // command 7 , command != 7 
                                }
                            }
                            else
                            {
                                byte ret = 0;
                                switch (ComRes)
                                {
                                    case 0: ret = 50; break;    // command 7  Communication Nothing Happened.
                                    //case 1: ret = 51; break;    // command 7  Communication All steps OK!               
                                    case 2: ret = 52; break;    // command 7  Communication Decoding Information Error  
                                    case 3: ret = 53; break;    // command 7  Communication Data Receiving Error        
                                    case 4: ret = 54; break;    // command 7 Communication Data Sending Error          
                                    case 5: ret = 55; break;    // command 7  Communication Dongle Not Present          
                                    case 6: ret = 56; break;    // command 7  Communication Other Error                 
                                }
                                return ret; // command 7 communication error
                            }
                        }
                    }
                    else if (remoteCMD == 8)
                    {
                        localEMSCTR++;
                        ComRes = CommunicationDongle(serialNo, false);
                        if (ComRes == 1)
                        {
                            if (remoteCMD == 7)
                            {
                                if (remoteDongleCTR > localDongleCTR)
                                {
                                    localDongleCTR = remoteDongleCTR;
                                    localEMSCTR++;
                                    upDateRegistry();
                                    return 6; // command 8  command 7 remote cnt > local cnt
                                }
                                else
                                {
                                    localDongleCTR = remoteDongleCTR;
                                    localEMSCTR++;
                                    ComRes = CommunicationDongle(serialNo, true);
                                    if (ComRes == 1)
                                    {
                                        if (remoteCMD == 7)
                                        {
                                            if (remoteDongleCTR > localDongleCTR)
                                            {
                                                localDongleCTR = remoteDongleCTR;
                                                localEMSCTR++;
                                                upDateRegistry();
                                                return 7; //command 8 , command 7 , command 7 remote cnt > local cnt 
                                            }
                                            else
                                            {
                                                return 8; //command 8 , command 7 , command 7 remote cnt !> local cnt 
                                            }
                                        }
                                        else
                                        {
                                            return 9; //command 8 ,  command 7 ,command != 7 
                                        }
                                    }
                                    else
                                    {
                                        byte ret = 0;
                                        switch (ComRes)
                                        {
                                            case 0: ret = 30; break;    // command 8 ,  command 7  Communication Nothing Happened.
                                            //case 1: ret = 31; break;    // command 8 ,  command 7  Communication All steps OK!               
                                            case 2: ret = 32; break;    // command 8 ,  command 7  Communication Decoding Information Error  
                                            case 3: ret = 33; break;    // command 8 ,  command 7  Communication Data Receiving Error        
                                            case 4: ret = 34; break;    // command 8 ,  command 7  Communication Data Sending Error          
                                            case 5: ret = 35; break;    // command 8 ,  command 7  Communication Dongle Not Present          
                                            case 6: ret = 36; break;    // command 8 ,  command 7  Communication Other Error                 
                                        }
                                        return ret; // command 8 ,  command 7 communication error
                                    }
                                }
                            }
                            else if (remoteCMD == 6)
                            {
                                return 11;  // command 8 command = 6 error.
                            }
                            else
                            {
                                return 12; //  command 8 command != 7,6.
                            }
                        }
                        else
                        {
                            byte ret = 0;
                            switch (ComRes)
                            {
                                case 0: ret = 40; break;    // command 8  Communication Nothing Happened.
                                //case 1: ret = 41; break;    // command 8  Communication All steps OK!               
                                case 2: ret = 42; break;    // command 8  Communication Decoding Information Error  
                                case 3: ret = 43; break;    // command 8  Communication Data Receiving Error        
                                case 4: ret = 44; break;    // command 8  Communication Data Sending Error          
                                case 5: ret = 45; break;    // command 8  Communication Dongle Not Present          
                                case 6: ret = 46; break;    // command 8  Communication Other Error                 
                            }
                            return ret; // command 8 communication error.
                        }
                    }
                    else if (remoteCMD == 4)
                    {
                        localDongleCTR = remoteDongleCTR;
                        localEMSCTR++;
                        upDateRegistry();
                        return 14; // command 4 OK.
                    }
                    else if (remoteCMD == 6)
                    {
                        return 15; // command 6 error
                    }
                    else
                    {
                        return 16; // command != 4,6,7,8....
                    }
                }
                else
                {
                    byte ret = 0;
                    switch (ComRes)
                    {
                        case 0: ret = 20; break;    //First Communication Nothing Happened.
                        //case 1: ret = 21; break;    //First Communication All steps OK!               
                        case 2: ret = 22; break;    //First Communication Decoding Information Error  
                        case 3: ret = 23; break;    //First Communication Data Receiving Error        
                        case 4: ret = 24; break;    //First Communication Data Sending Error          
                        case 5: ret = 25; break;    //First Communication Dongle Not Present          
                        case 6: ret = 26; break;    //First Communication Other Error                 
                    }
                    return ret; // First Communication error
                }
            }
            else
            {
                return 18; // Read Registry Error
            }
        }
        //========================================================================================
        public bool readRegistry()
        {
            try
            {
                byte[] emsData = new byte[7];
                byte[] dongleData = new byte[7];
                uint checkSum;
                RegistryKey rgKey;
                #region Check For Registry
                try
                {
                    if (Registry.CurrentUser.GetSubKeyNames().Contains<string>(subKey1))
                    {
                        rgKey = Registry.CurrentUser.OpenSubKey(subKey1);
                        if (rgKey.GetSubKeyNames().Contains<string>(subKey2))
                        {
                            rgKey = rgKey.OpenSubKey(subKey2);
                            if (rgKey.GetValueNames().Contains<string>(regEMSCounter))
                            {
                                if (rgKey.GetValueKind(regEMSCounter) != RegistryValueKind.Binary)
                                {
                                    // invalid Data Type
                                    makePacket(emsData, 10);
                                    Registry.SetValue(keyName, regEMSCounter, emsData, RegistryValueKind.Binary);
                                }// end if invalid Data Type
                            }
                            else
                            {
                                // EMS Counter Not Exist
                                makePacket(emsData, 10);
                                Registry.SetValue(keyName, regEMSCounter, emsData, RegistryValueKind.Binary);
                            }// end if EMS Counter Not Exist
                            if (rgKey.GetValueNames().Contains<string>(regDongleCounter))
                            {
                                // Dongle Counter Exist
                                // now check the Dongle Counter Type
                                if (rgKey.GetValueKind(regDongleCounter) != RegistryValueKind.Binary)
                                {
                                    // Invalid Data Type
                                    makePacket(dongleData, 0);
                                    Registry.SetValue(keyName, regDongleCounter, dongleData, RegistryValueKind.Binary);
                                }// end if Invalid Data Type
                            } // end if Dongle Counter Exist
                            else
                            {
                                // Dongle Counter Not Exist
                                makePacket(dongleData, 0);
                                Registry.SetValue(keyName, regDongleCounter, dongleData, RegistryValueKind.Binary);
                            } // end if Dongle Not Exist
                        }// end if Key Exist
                        else
                        {
                            makePacket(emsData, 10);
                            Registry.SetValue(keyName, regEMSCounter, emsData, RegistryValueKind.Binary);
                            makePacket(dongleData, 0);
                            Registry.SetValue(keyName, regDongleCounter, dongleData, RegistryValueKind.Binary);
                        }
                    }
                    else
                    {
                        makePacket(emsData, 10);
                        Registry.SetValue(keyName, regEMSCounter, emsData, RegistryValueKind.Binary);
                        makePacket(dongleData, 0);
                        Registry.SetValue(keyName, regDongleCounter, dongleData, RegistryValueKind.Binary);
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
                #endregion
                #region Read Registry And Check
                try
                {
                    emsData = (byte[])Registry.GetValue(keyName, regEMSCounter, new byte[7]);
                    dongleData = (byte[])Registry.GetValue(keyName, regDongleCounter, new byte[7]);
                }
                catch (Exception e)
                {
                    return false;
                }
                // Now Decrypt the value and check the checkSum

                byte decryptRow;

                decryptRow = emsData[0];

                emsData[1] ^= EncryptTable[decryptRow][0];
                emsData[2] ^= EncryptTable[decryptRow][1];
                emsData[3] ^= EncryptTable[decryptRow][2];
                emsData[4] ^= EncryptTable[decryptRow][3];
                emsData[5] ^= EncryptTable[decryptRow][4];
                emsData[6] ^= EncryptTable[decryptRow][5];

                checkSum = 89;
                checkSum += emsData[0];
                checkSum += emsData[1];
                checkSum += emsData[2];
                checkSum += emsData[3];
                checkSum += emsData[4];

                convertType.byte1 = emsData[5];
                convertType.byte2 = emsData[6];
                convertType.byte3 = 0;
                convertType.byte4 = 0;

                if (checkSum != convertType.intMem)
                {
                    // invalid checkSum
                    // store the Default Value
                    makePacket(emsData, 10);
                    Registry.SetValue(keyName, regEMSCounter, emsData, RegistryValueKind.Binary);
                    localEMSCTR = 10;
                }// end if invalid checkSum
                else
                {
                    // valid checkSum
                    convertType.byte1 = emsData[1];
                    convertType.byte2 = emsData[2];
                    convertType.byte3 = emsData[3];
                    convertType.byte4 = emsData[4];
                    localEMSCTR = convertType.intMem;
                }// end if valid checkSum

                decryptRow = dongleData[0];
                dongleData[1] ^= EncryptTable[decryptRow][0];
                dongleData[2] ^= EncryptTable[decryptRow][1];
                dongleData[3] ^= EncryptTable[decryptRow][2];
                dongleData[4] ^= EncryptTable[decryptRow][3];
                dongleData[5] ^= EncryptTable[decryptRow][4];
                dongleData[6] ^= EncryptTable[decryptRow][5];

                checkSum = 89;
                checkSum += dongleData[0];
                checkSum += dongleData[1];
                checkSum += dongleData[2];
                checkSum += dongleData[3];
                checkSum += dongleData[4];

                convertType.byte1 = dongleData[5];
                convertType.byte2 = dongleData[6];
                convertType.byte3 = 0;
                convertType.byte4 = 0;

                if (checkSum != convertType.intMem)
                {
                    // invalid checkSum

                    makePacket(dongleData, 0);
                    Registry.SetValue(keyName, regDongleCounter, dongleData, RegistryValueKind.Binary);
                    localDongleCTR = 0;
                }// end if invalid checkSum
                else
                {
                    // Valid CheckSum
                    convertType.byte1 = dongleData[1];
                    convertType.byte2 = dongleData[2];
                    convertType.byte3 = dongleData[3];
                    convertType.byte4 = dongleData[4];
                    localDongleCTR = convertType.intMem;
                } // end if Valid CheckSum
                return true;
                #endregion
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
        //========================================================================================
        public bool upDateRegistry()
        {
            byte[] emsData = new byte[7];
            byte[] dongleData = new byte[7];
            RegistryKey rgKey;
            #region Check For Registry
            try
            {
                if (Registry.CurrentUser.GetSubKeyNames().Contains<string>(subKey1))
                {
                    rgKey = Registry.CurrentUser.OpenSubKey(subKey1);
                    if (rgKey.GetSubKeyNames().Contains<string>(subKey2))
                    {
                        rgKey = rgKey.OpenSubKey(subKey2);
                        if (rgKey.GetValueNames().Contains<string>(regEMSCounter))
                        {
                            if (rgKey.GetValueKind(regEMSCounter) != RegistryValueKind.Binary)
                            {
                                // invalid Data Type
                                makePacket(emsData, 10);
                                Registry.SetValue(keyName, regEMSCounter, emsData, RegistryValueKind.Binary);
                            }// end if invalid Data Type
                        }
                        else
                        {
                            // EMS Counter Not Exist
                            makePacket(emsData, 10);
                            Registry.SetValue(keyName, regEMSCounter, emsData, RegistryValueKind.Binary);
                        }// end if EMS Counter Not Exist
                        if (rgKey.GetValueNames().Contains<string>(regDongleCounter))
                        {
                            // Dongle Counter Exist
                            // now check the Dongle Counter Type
                            if (rgKey.GetValueKind(regDongleCounter) != RegistryValueKind.Binary)
                            {
                                // Invalid Data Type
                                makePacket(dongleData, 0);
                                Registry.SetValue(keyName, regDongleCounter, dongleData, RegistryValueKind.Binary);
                            }// end if Invalid Data Type
                        } // end if Dongle Counter Exist
                        else
                        {
                            // Dongle Counter Not Exist
                            makePacket(dongleData, 0);
                            Registry.SetValue(keyName, regDongleCounter, dongleData, RegistryValueKind.Binary);
                        } // end if Dongle Not Exist
                    }// end if Key Exist
                    else
                    {
                        makePacket(emsData, 10);
                        Registry.SetValue(keyName, regEMSCounter, emsData, RegistryValueKind.Binary);
                        makePacket(dongleData, 0);
                        Registry.SetValue(keyName, regDongleCounter, dongleData, RegistryValueKind.Binary);
                    }
                }
                else
                {
                    makePacket(emsData, 10);
                    Registry.SetValue(keyName, regEMSCounter, emsData, RegistryValueKind.Binary);
                    makePacket(dongleData, 0);
                    Registry.SetValue(keyName, regDongleCounter, dongleData, RegistryValueKind.Binary);
                }

                makePacket(emsData, localEMSCTR);
                Registry.SetValue(keyName, regEMSCounter, emsData, RegistryValueKind.Binary);
                makePacket(dongleData, localDongleCTR);
                Registry.SetValue(keyName, regDongleCounter, dongleData, RegistryValueKind.Binary);
            }
            catch
            {
                //clsCommExceptionLogger.SaveExceptionInLogFile("Registry Read", e.ToString());
                return false;
            }
            #endregion
            return false;
        }
        //========================================================================================
        public byte CommunicationDongle(byte[] serialPTR, bool firstTime)
        {
            /*************************************************
             *      Return Table Information                 *
             *************************************************   
             *                                               *
             *      1--> All steps OK!                       *
             *      2--> Decoding Information Error          *
             *      3--> Data Receiving Error                *
             *      4--> Data Sending Error                  *
             *      5--> Dongle Not Present                  *
             *      6--> Other Error                         *
             *      7-->                                     *
             *      8-->                                     *
             *                                               *
             *************************************************/
            byte encryptedRow;
            uint ctr;
            bool res;
            clearBuffer();

            byte counterRow;
            uint checkSum;
            Random rand = new Random();
            counterRow = (byte)rand.Next(16);
            counterRow &= 0x0F;
            checkSum = 79;
            #region Save Ems Counter
            convertType.intMem = (localEMSCTR + 16);

            counterBuffer[0] = counterRow;
            counterBuffer[1] = convertType.byte1;
            counterBuffer[2] = convertType.byte2;
            counterBuffer[3] = convertType.byte3;
            counterBuffer[4] = convertType.byte4;

            checkSum += (uint)(counterBuffer[0] + counterBuffer[1] + counterBuffer[2] + counterBuffer[3] +
                         counterBuffer[4]);

            convertType.intMem = checkSum;
            counterBuffer[5] = convertType.byte1;
            counterBuffer[6] = convertType.byte2;

            //makeShowData(counterBuffer, 7, 2);

            counterBuffer[1] ^= EncryptTable[counterRow][0];
            counterBuffer[2] ^= EncryptTable[counterRow][1];
            counterBuffer[3] ^= EncryptTable[counterRow][2];
            counterBuffer[4] ^= EncryptTable[counterRow][3];
            counterBuffer[5] ^= EncryptTable[counterRow][4];
            counterBuffer[6] ^= EncryptTable[counterRow][5];
            #endregion
            #region Make Serial Buffer
            byte serialRow;

            serialRow = (byte)rand.Next(16);
            serialRow &= 0x0F;

            serialBuffer[0] = serialRow;
            checkSum = 79;
            checkSum += serialBuffer[0];

            for (int i = 1; i <= 24; i++)
            {
                serialBuffer[i] = serialPTR[(i - 1)];
                checkSum += serialBuffer[i];
            }

            convertType.intMem = checkSum;
            serialBuffer[25] = convertType.byte1;
            serialBuffer[26] = convertType.byte2;

            //makeShowData(serialBuffer, 27, 2);

            serialBuffer[1] ^= EncryptTable[serialRow][0];
            serialBuffer[2] ^= EncryptTable[serialRow][1];
            serialBuffer[3] ^= EncryptTable[serialRow][2];
            serialBuffer[4] ^= EncryptTable[serialRow][3];
            serialBuffer[5] ^= EncryptTable[serialRow][4];
            serialBuffer[6] ^= EncryptTable[serialRow][5];
            serialBuffer[7] ^= EncryptTable[serialRow][6];
            serialBuffer[8] ^= EncryptTable[serialRow][7];
            serialBuffer[9] ^= EncryptTable[serialRow][8];
            serialBuffer[10] ^= EncryptTable[serialRow][9];
            serialBuffer[11] ^= EncryptTable[serialRow][10];
            serialBuffer[12] ^= EncryptTable[serialRow][11];
            serialBuffer[13] ^= EncryptTable[serialRow][12];
            serialBuffer[14] ^= EncryptTable[serialRow][13];
            serialBuffer[15] ^= EncryptTable[serialRow][14];
            serialBuffer[16] ^= EncryptTable[serialRow][15];
            serialBuffer[17] ^= EncryptTable[serialRow][0];
            serialBuffer[18] ^= EncryptTable[serialRow][1];
            serialBuffer[19] ^= EncryptTable[serialRow][2];
            serialBuffer[20] ^= EncryptTable[serialRow][3];
            serialBuffer[21] ^= EncryptTable[serialRow][4];
            serialBuffer[22] ^= EncryptTable[serialRow][5];
            serialBuffer[23] ^= EncryptTable[serialRow][6];
            serialBuffer[24] ^= EncryptTable[serialRow][7];
            serialBuffer[25] ^= EncryptTable[serialRow][8];
            serialBuffer[26] ^= EncryptTable[serialRow][9];
            #endregion
            #region Make Command Buffer
            byte cmdRow;

            cmdRow = (byte)rand.Next(16);
            cmdRow &= 0x0F;

            commandBuffer[0] = cmdRow;
            commandBuffer[1] = 59;
            commandBuffer[2] = 0;

            checkSum = 79;
            checkSum += (uint)(commandBuffer[0] + commandBuffer[1] + commandBuffer[2]);
            convertType.intMem = checkSum;

            commandBuffer[3] = convertType.byte1;
            commandBuffer[4] = convertType.byte2;

            //makeShowData(commandBuffer, 5, 2);

            commandBuffer[1] ^= EncryptTable[cmdRow][0];
            commandBuffer[2] ^= EncryptTable[cmdRow][1];
            commandBuffer[3] ^= EncryptTable[cmdRow][2];
            commandBuffer[4] ^= EncryptTable[cmdRow][3];
            #endregion
            #region Make OutPut Buffer with Random Blocks

            byte randomLen;
            byte randomCTR;
            byte tempRandom;
            byte resTemp;

            encryptedRow = (byte)rand.Next(16);
            encryptedRow |= 1;
            encryptedRow = (byte)(encryptedRow & 0x0F);

            bufferCTR = 0;
            outputBuffer[bufferCTR++] = encryptedRow;
            // Random Block 1

            tempRandom = (byte)rand.Next(255);
            tempRandom |= 1;
            randomLen = (byte)(tempRandom & 0x0F);

            //outputBuffer[bufferCTR++] = tempRandom;
            outputBuffer[bufferCTR++] = randomLen;

            for (randomCTR = 0; randomCTR < randomLen; randomCTR++)
            {
                outputBuffer[bufferCTR++] = (byte)rand.Next(255);
            }// end for loop

            // move counter

            outputBuffer[bufferCTR++] = counterBuffer[0];
            outputBuffer[bufferCTR++] = counterBuffer[1];
            outputBuffer[bufferCTR++] = counterBuffer[2];
            outputBuffer[bufferCTR++] = counterBuffer[3];
            outputBuffer[bufferCTR++] = counterBuffer[4];
            outputBuffer[bufferCTR++] = counterBuffer[5];
            outputBuffer[bufferCTR++] = counterBuffer[6];
            // Random Block 2

            tempRandom = (byte)rand.Next(255);
            tempRandom |= 1;
            randomLen = (byte)(tempRandom & 0x0F);

            outputBuffer[bufferCTR++] = randomLen;
            for (randomCTR = 0; randomCTR < randomLen; randomCTR++)
            {
                outputBuffer[bufferCTR++] = (byte)rand.Next(255);
            }// end for loop

            // move command

            outputBuffer[bufferCTR++] = commandBuffer[0];
            outputBuffer[bufferCTR++] = commandBuffer[1];
            outputBuffer[bufferCTR++] = commandBuffer[2];
            outputBuffer[bufferCTR++] = commandBuffer[3];
            outputBuffer[bufferCTR++] = commandBuffer[4];
            // Random Block 3

            tempRandom = (byte)rand.Next(255);
            tempRandom |= 1;
            randomLen = (byte)(tempRandom & 0x0F);
            outputBuffer[bufferCTR++] = randomLen;
            for (randomCTR = 0; randomCTR < randomLen; randomCTR++)
            {
                outputBuffer[bufferCTR++] = (byte)rand.Next(255);
            }// end for loop

            // move serial
            outputBuffer[bufferCTR++] = serialBuffer[0];
            outputBuffer[bufferCTR++] = serialBuffer[1];
            outputBuffer[bufferCTR++] = serialBuffer[2];
            outputBuffer[bufferCTR++] = serialBuffer[3];
            outputBuffer[bufferCTR++] = serialBuffer[4];
            outputBuffer[bufferCTR++] = serialBuffer[5];
            outputBuffer[bufferCTR++] = serialBuffer[6];
            outputBuffer[bufferCTR++] = serialBuffer[7];
            outputBuffer[bufferCTR++] = serialBuffer[8];
            outputBuffer[bufferCTR++] = serialBuffer[9];
            outputBuffer[bufferCTR++] = serialBuffer[10];
            outputBuffer[bufferCTR++] = serialBuffer[11];
            outputBuffer[bufferCTR++] = serialBuffer[12];
            outputBuffer[bufferCTR++] = serialBuffer[13];
            outputBuffer[bufferCTR++] = serialBuffer[14];
            outputBuffer[bufferCTR++] = serialBuffer[15];
            outputBuffer[bufferCTR++] = serialBuffer[16];
            outputBuffer[bufferCTR++] = serialBuffer[17];
            outputBuffer[bufferCTR++] = serialBuffer[18];
            outputBuffer[bufferCTR++] = serialBuffer[19];
            outputBuffer[bufferCTR++] = serialBuffer[20];
            outputBuffer[bufferCTR++] = serialBuffer[21];
            outputBuffer[bufferCTR++] = serialBuffer[22];
            outputBuffer[bufferCTR++] = serialBuffer[23];
            outputBuffer[bufferCTR++] = serialBuffer[24];
            outputBuffer[bufferCTR++] = serialBuffer[25];
            outputBuffer[bufferCTR++] = serialBuffer[26];

            // Random Block 4

            tempRandom = (byte)rand.Next(255);
            tempRandom |= 1;
            randomLen = (byte)(tempRandom & 0x0F);
            outputBuffer[bufferCTR++] = randomLen;
            for (randomCTR = 0; randomCTR < randomLen; randomCTR++)
            {
                outputBuffer[bufferCTR++] = (byte)rand.Next(255);
            }// end for loop

            byte colIndex = 0;
            for (ctr = 1; ctr < bufferCTR; ctr++)
            {
                outputBuffer[ctr] ^= EncryptTable[encryptedRow][colIndex++];
                if (colIndex == 16)
                {
                    colIndex = 0;
                }
            }// end for loop
            #endregion
            #region Send OutPut Buffer to Dongle
            if (firstTime)
            {
                res = donglePresent();
            }
            else
            {
                res = true;
            }
            if (res)
            {
                // dongle present

                res = SendOutputBufferToDongle();
                #region Check for Recieved Data
                if (res)
                {
                    //System.Threading.Thread.Sleep(100);
                    objComPort.DiscardInBuffer();
                    res = receivedFromDongle();
                    if (res)
                    {
                        resTemp = decodeInputBuffer();
                        if (resTemp == 2)
                        {
                            //sendDataToShow(lstDataShowRX, lstDataShowTX);
                            return 1; // All steps OK!
                        }
                        else
                        {
                            return 2; // Decoding Information Error
                        }
                    }
                    else
                    {
                        return 3; // Data Receiving Error
                    }
                }
                else
                {
                    return 4; // Data Sending Error
                }
                #endregion
            } // end if dongle present
            else
            {
                return 5; // Dongle Not Present
            }
            #endregion
            //return 6;   // Other Error
        }
        //========================================================================================
        public bool SendOutputBufferToDongle()
        {

            objComPort.DiscardOutBuffer();
            for (uint i = 0; i < bufferCTR; i++)
            {
                commBuffer[i] = outputBuffer[i];
            }

            return writeToDongle((int)bufferCTR);
        }
        //========================================================================================
        private bool receivedFromDongle()
        {
            while (objComPort.BytesToRead < 15) ;
            int bytes = 0;
            int timeOut = 15;
            bool begin = false, retVal = false;

            objComPort.ReadTimeout = 15;
            while ((bytes < 62))
            {
                if (timeOut == 0) break;
                try
                {
                    recDataBuffer[bytes++] = (byte)objComPort.ReadByte();
                    timeOut = 15;
                    begin = true;
                }
                catch (Exception ex)
                {
                    if (begin)
                    {
                        if (bytes >= 19)
                            retVal = true;
                        else
                            retVal = false;
                        break;
                    }
                    else
                    {
                        timeOut--;
                    }
                }
                
                System.Threading.Thread.Sleep(3);
            }
            retVal = true;//AHMED
            if (timeOut == 0)
                return false;

            recDataBuffer.CopyTo(inputBuffer, 0);
            return retVal;
        }
        //========================================================================================
        private byte decodeInputBuffer()
        {
            /*
              return 0 = invalid Input Buffer
              return 1 = buffer Verified
            */

            byte decodeRow, ctr, colIndex;
            byte dongleCTRIndex, dongleCMDIndex;

            decodeRow = inputBuffer[0];
            decodeRow &= 0x0F;
            //makeShowData(inputBuffer,inputBuffer.Length,1);
            colIndex = 0;
            for (ctr = 1; ctr < 62; ctr++)
            {
                inputBuffer[ctr] ^= EncryptTable[decodeRow][colIndex++];
                if (colIndex == 16)
                {
                    colIndex = 0;
                }
            }// end for loop

            decodeRow = inputBuffer[1];
            decodeRow &= 0x0F;

            dongleCTRIndex = (byte)(decodeRow + 2);

            counterBuffer[0] = inputBuffer[dongleCTRIndex++];
            counterBuffer[1] = inputBuffer[dongleCTRIndex++];
            counterBuffer[2] = inputBuffer[dongleCTRIndex++];
            counterBuffer[3] = inputBuffer[dongleCTRIndex++];
            counterBuffer[4] = inputBuffer[dongleCTRIndex++];
            counterBuffer[5] = inputBuffer[dongleCTRIndex++];
            counterBuffer[6] = inputBuffer[dongleCTRIndex++];

            decodeRow = inputBuffer[dongleCTRIndex];
            decodeRow &= 0x0F;

            dongleCMDIndex = (byte)(dongleCTRIndex + decodeRow + 1);

            commandBuffer[0] = inputBuffer[dongleCMDIndex++];
            commandBuffer[1] = inputBuffer[dongleCMDIndex++];
            commandBuffer[2] = inputBuffer[dongleCMDIndex++];
            commandBuffer[3] = inputBuffer[dongleCMDIndex++];
            commandBuffer[4] = inputBuffer[dongleCMDIndex++];

            // Verify Counter

            byte rowNo;
            uint checkSum;

            rowNo = counterBuffer[0];
            rowNo &= 0x0F;

            counterBuffer[1] ^= EncryptTable[rowNo][0];
            counterBuffer[2] ^= EncryptTable[rowNo][1];
            counterBuffer[3] ^= EncryptTable[rowNo][2];
            counterBuffer[4] ^= EncryptTable[rowNo][3];
            counterBuffer[5] ^= EncryptTable[rowNo][4];
            counterBuffer[6] ^= EncryptTable[rowNo][5];

            checkSum = 79;
            checkSum += counterBuffer[0];
            checkSum += counterBuffer[1];
            checkSum += counterBuffer[2];
            checkSum += counterBuffer[3];
            checkSum += counterBuffer[4];

            convertType.byte1 = counterBuffer[5];
            convertType.byte2 = counterBuffer[6];
            convertType.byte3 = 0;
            convertType.byte4 = 0;

            if (checkSum != convertType.intMem)
            {
                // ShowMessage("Serial Check Sum Error");
                return 0;
            }

            rowNo = commandBuffer[0];
            rowNo &= 0x0F;

            commandBuffer[1] ^= EncryptTable[rowNo][0];
            commandBuffer[2] ^= EncryptTable[rowNo][1];
            commandBuffer[3] ^= EncryptTable[rowNo][2];
            commandBuffer[4] ^= EncryptTable[rowNo][3];

            checkSum = 79;
            checkSum += commandBuffer[0];
            checkSum += commandBuffer[1];
            checkSum += commandBuffer[2];

            convertType.byte1 = commandBuffer[3];
            convertType.byte2 = commandBuffer[4];
            convertType.byte3 = 0;
            convertType.byte4 = 0;

            if (checkSum != convertType.intMem)
            {
                // ShowMessage("Command Check Sum Error");
                return 1;
            }

            convertType.byte1 = counterBuffer[1];
            convertType.byte2 = counterBuffer[2];
            convertType.byte3 = 0;
            convertType.byte4 = 0;

            // EditDongleCounter->Text = convertType.intData; //??
            remoteDongleCTR = convertType.intMem;

            convertType.byte1 = commandBuffer[1];
            convertType.byte2 = commandBuffer[2];
            convertType.byte3 = 0;
            convertType.byte4 = 0;

            // EditDongleCommand->Text = convertType.intData; // ???????
            remoteCMD = convertType.intMem;

            return 2;

        }
        //========================================================================================
        public void clearBuffer()
        {
            uint i;
            for (i = 0; i < 100; i++)
            {
                inputBuffer[i] = 0;
            }
            for (i = 0; i < 100; i++)
            {
                recDataBuffer[i] = 0;
            }

            for (i = 0; i < 150; i++)
            {
                outputBuffer[i] = 0;
            }

            for (i = 0; i < 27; i++)
            {
                serialBuffer[i] = 0;
            }
            //lstDataShowRX.Clear();
            //lstDataShowTX.Clear();

        }
        //========================================================================================
        public bool makePacket(byte[] regData, uint data)
        {
            byte encryptRow;
            uint checkSum;
            Random rand = new Random(16);
            encryptRow = (byte)(rand.Next(0, 16));
            encryptRow &= 0x0F;
            regData[0] = encryptRow; // Encrypt Row No

            convertType.intMem = data;

            regData[1] = convertType.byte1; // EMS Counter
            regData[2] = convertType.byte2; // EMS Counter
            regData[3] = convertType.byte3; // EMS Counter
            regData[4] = convertType.byte4; // EMS Counter

            checkSum = 89;

            checkSum += regData[0];
            checkSum += regData[1];
            checkSum += regData[2];
            checkSum += regData[3];
            checkSum += regData[4];

            convertType.intMem = checkSum;

            regData[5] = convertType.byte1;  // checkSum 0
            regData[6] = convertType.byte2;  // checkSum 1

            regData[1] ^= EncryptTable[encryptRow][0];
            regData[2] ^= EncryptTable[encryptRow][1];
            regData[3] ^= EncryptTable[encryptRow][2];
            regData[4] ^= EncryptTable[encryptRow][3];
            regData[5] ^= EncryptTable[encryptRow][4];
            regData[6] ^= EncryptTable[encryptRow][5];
            return true;
        }
        //========================================================================================
        public bool setPort(string portName)
        {

            objComPort.Close();
            objComPort.PortName = portName;
            objComPort.BaudRate = 2400;
            objComPort.Parity = Parity.None;
            objComPort.ReceivedBytesThreshold = 1;
            objComPort.WriteBufferSize = 2048;
            objComPort.ReadBufferSize = 2048;
            objComPort.RtsEnable = true;
            objComPort.DtrEnable = true;
            objComPort.Handshake = 0;
            try
            {
                objComPort.Open();
                return true;
            }
            catch
            {

                return false;
            }
        }
        //========================================================================================
        public bool closePort()
        {
            try
            {
                if (objComPort.IsOpen)
                {
                    objComPort.Close();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //========================================================================================
        public bool donglePresent()
        {
            objComPort.RtsEnable = false;
            System.Threading.Thread.Sleep(500);
            objComPort.RtsEnable = true;

            int bytes = 0;
            int timeOut = 15;
            objComPort.ReadTimeout = 15;
            while ((bytes < 25))
            {
                if (timeOut == 0) break;
                try
                {
                    recBuffer[bytes++] = (byte)objComPort.ReadByte();
                    timeOut = 15;
                }
                catch
                {
                    timeOut--;
                }
                System.Threading.Thread.Sleep(1);
            }
            if (timeOut == 0)
                return false;
            else
            {
                commBuffer[0] = 170;
                commBuffer[1] = 170;
                commBuffer[2] = 170;
                commBuffer[3] = 170;
                commBuffer[4] = 170;
                commBuffer[5] = 170;
                commBuffer[6] = 170;
                commBuffer[7] = 170;
                commBuffer[8] = 170;
                commBuffer[9] = 170;
                commBuffer[10] = 170;
                commBuffer[11] = 170;
                commBuffer[12] = 5;   // login complete Here
                return writeToDongle(13);
            }
        }
        //========================================================================================
        public bool writeToDongle(int Length)
        {
            byte[] sendbyte = new byte[2];
            try
            {
                objComPort.DiscardInBuffer();
                for (int a = 0; a < Length; a++)
                {
                    sendbyte[0] = commBuffer[a];
                    objComPort.Write(sendbyte, 0, 1);
                    System.Threading.Thread.Sleep(5);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        //========================================================================================
        public byte[] makeSerialNoPacket(byte[] serialkey, byte[] softwareID)
        {
            byte[] emsHddSerialCmb = new byte[24];
            for (byte a = 0; a < 24; a++)
                emsHddSerialCmb[a] = (byte)'\0';

            for (byte a = 0; a < serialkey.Length; a++)
                emsHddSerialCmb[a] = (byte)(serialkey[a]);

            for (byte a = 0; a < softwareID.Length; a++)
                emsHddSerialCmb[a + 16] = (byte)(softwareID[a]);

            return emsHddSerialCmb;
        }
        public bool checkForDongleSecurity(byte[] serialPacket)
        {
            int res = 0;
            string msg = string.Empty;
            bool dongleStatus = false;
            res = this.dongleStatus(serialPacket,"COM4");
            switch (res)
            {
                case 0: msg = "Ems and HDD Serial saving Error"; dongleStatus = false; break;
                case 1: msg = "command 7 and remote cnt > local cnt."; dongleStatus = true; break;
                case 2: msg = "command 7 , command 7 and remote cnt == local cnt"; dongleStatus = true; break;
                case 3: msg = "command 7 , command 7 and remote cnt !> local cnt"; dongleStatus = false; break;
                case 4: msg = "command 7 , command != 7"; dongleStatus = false; break;
                case 5: msg = "command 7 communication error"; dongleStatus = false; break;
                case 6: msg = "command 8 , command 7   remote cnt > local cnt"; dongleStatus = true; break;
                case 7: msg = "command 8 , command 7 , command 7 remote cnt > local cnt"; dongleStatus = true; break;
                case 8: msg = "command 8 , command 7 , command 7 remote cnt !> local cnt"; dongleStatus = false; break;
                case 9: msg = "command 8 , command 7 , command != 7"; dongleStatus = false; break;
                case 10: msg = "command 8 , command 7 communication error"; dongleStatus = false; break;
                case 11: msg = "command 8 command = 6 error."; dongleStatus = false; break;
                case 12: msg = "command 8 command != 7,6."; dongleStatus = false; break;
                case 13: msg = "command 8 communication error."; dongleStatus = false; break;
                case 14: msg = "command 4 OK."; dongleStatus = true; break;
                case 15: msg = "command 6 error"; dongleStatus = false; break;
                case 16: msg = "command != 4,6,7,8...."; dongleStatus = false; break;
                case 17: msg = "First Communication error"; dongleStatus = false; break;
                case 18: msg = "Read Registry Error"; dongleStatus = false; break;

                case 20: msg = "First Communication Nothing Happened. "; dongleStatus = false; break;
                case 21: msg = "First Communication All steps OK!"; dongleStatus = false; break;
                case 22: msg = "First Communication Decoding Information Error"; dongleStatus = false; break;
                case 23: msg = "First Communication Data Receiving Error"; dongleStatus = false; break;
                case 24: msg = "First Communication Data Sending Error"; dongleStatus = false; break;
                case 25: msg = "First Communication Dongle Not Present"; dongleStatus = false; break;
                case 26: msg = "First Communication Other Error"; dongleStatus = false; break;

                case 30: msg = "command 8 ,  command 7  Communication Nothing Happened. "; dongleStatus = false; break;
                case 31: msg = "command 8 ,  command 7  Communication All steps OK!"; dongleStatus = false; break;
                case 32: msg = "command 8 ,  command 7  Communication Decoding Information Error"; dongleStatus = false; break;
                case 33: msg = "command 8 ,  command 7  Communication Data Receiving Error"; dongleStatus = false; break;
                case 34: msg = "command 8 ,  command 7  Communication Data Sending Error"; dongleStatus = false; break;
                case 35: msg = "command 8 ,  command 7  Communication Dongle Not Present"; dongleStatus = false; break;
                case 36: msg = "command 8 ,  command 7  Communication Other Error"; dongleStatus = false; break;

                case 40: msg = "command 8  Communication Nothing Happened. "; dongleStatus = false; break;
                case 41: msg = "command 8  Communication All steps OK!"; dongleStatus = false; break;
                case 42: msg = "command 8  Communication Decoding Information Error"; dongleStatus = false; break;
                case 43: msg = "command 8  Communication Data Receiving Error"; dongleStatus = false; break;
                case 44: msg = "command 8  Communication Data Sending Error"; dongleStatus = false; break;
                case 45: msg = "command 8  Communication Dongle Not Present"; dongleStatus = false; break;
                case 46: msg = "command 8  Communication Other Error"; dongleStatus = false; break;

                case 50: msg = "command 7  Communication Nothing Happened. "; dongleStatus = false; break;
                case 51: msg = "command 7  Communication All steps OK!"; dongleStatus = false; break;
                case 52: msg = "command 7  Communication Decoding Information Error"; dongleStatus = false; break;
                case 53: msg = "command 7  Communication Data Receiving Error"; dongleStatus = false; break;
                case 54: msg = "command 7  Communication Data Sending Error"; dongleStatus = false; break;
                case 55: msg = "command 7  Communication Dongle Not Present"; dongleStatus = false; break;
                case 56: msg = "command 7  Communication Other Error"; dongleStatus = false; break;
            }
            System.Threading.Thread.Sleep(500);
            if (dongleStatus)
                return dongleStatus;

            return false;
        }

        //========================================================================================
        //public void makeShowData(byte[] data, int length, byte Mode)
        //{
        //    stDataShow row;
        //    for (int i = 0; i < length; i++)
        //    {
        //        row = new stDataShow();
        //        row.Index = i.ToString();
        //        row.Chara = ((char)data[i]).ToString();
        //        row.Decimal = data[i].ToString();
        //        row.Hex = returnHex(data[i]);
        //        if (Mode == 1)
        //        {
        //            lstDataShowRX.Add(row);
        //        }
        //        else if (Mode == 2)
        //        {
        //            lstDataShowTX.Add(row);
        //        }
        //    }
        //}
        //=================================================================================
        private string returnHex(byte byt)
        {
            int Unit = byt % 16;
            int Tens = byt / 16;
            string retVal = "0x";

            if (Tens < 10)
                retVal += Tens.ToString();
            else
            {
                switch (Tens)
                {
                    case 10: retVal += "A"; break;
                    case 11: retVal += "B"; break;
                    case 12: retVal += "C"; break;
                    case 13: retVal += "D"; break;
                    case 14: retVal += "E"; break;
                    case 15: retVal += "F"; break;
                }
            }

            if (Unit < 10)
                retVal += Unit.ToString();
            else
            {
                switch (Unit)
                {
                    case 10: retVal += "A"; break;
                    case 11: retVal += "B"; break;
                    case 12: retVal += "C"; break;
                    case 13: retVal += "D"; break;
                    case 14: retVal += "E"; break;
                    case 15: retVal += "F"; break;
                }
            }

            return retVal;
        }
        public struct stInt
        {
            //[System.Runtime.InteropServices.FieldOffset(0)]
            public byte byte1;
            //[System.Runtime.InteropServices.FieldOffset(1)]
            public byte byte2;
            //[System.Runtime.InteropServices.FieldOffset(2)]
            public byte byte3;
            //[System.Runtime.InteropServices.FieldOffset(3)]
            public byte byte4;

            //[System.Runtime.InteropServices.FieldOffset(0)]
            public uint intMem
            {
                get
                {
                    IntMem = 0;
                    IntMem |= byte4; IntMem <<= 8;
                    IntMem |= byte3; IntMem <<= 8;
                    IntMem |= byte2; IntMem <<= 8;
                    IntMem |= byte1;
                    return IntMem;
                }
                set
                {
                    IntMem = value;
                    byte1 = (byte)(IntMem & 0xFF);
                    byte2 = (byte)((IntMem >> 8) & 0xFF);
                    byte3 = (byte)((IntMem >> 16) & 0xFF);
                    byte4 = (byte)((IntMem >> 24) & 0xFF);
                }
            }
            private uint IntMem;

        };
        //========================================================================================
    }
    public class stDataShow
    {
        private string _index;
        public string Index
        {
            get { return _index; }
            set { _index = value; }
        }

        private string _hex;
        public string Hex
        {
            get { return _hex; }
            set { _hex = value; }
        }
        private string _decimal;
        public string Decimal
        {
            get { return _decimal; }
            set { _decimal = value; }
        }
        private string _char;
        public string Chara
        {
            get { return _char; }
            set { _char = value; }
        }
        public stDataShow()
        { }
    }

}
