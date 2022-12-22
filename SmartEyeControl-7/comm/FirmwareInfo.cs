using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comm
{
    public class FirmwareInfo
    {
        internal byte[] modelID;
        internal byte[] model;
        internal byte[] company_code;
        internal byte[] serial_no;
        internal byte[] date;
        internal byte[] time;
        internal byte[] release_id;
        internal byte[] version;
        internal byte[] info_1;
        internal byte[] info_2;
        internal byte[] rfu;
        internal byte[] features;
        internal byte[] dlms_version;

        #region Properties

        public String ModelID
        {
            get
            {
                return Encoding.ASCII.GetString(modelID);
            }
            set
            {
                modelID = Encoding.ASCII.GetBytes(value);
            }
        }

        public String Model
        {
            get
            {
                return Encoding.ASCII.GetString(model);
            }
            set
            {
                model = Encoding.ASCII.GetBytes(value);
            }
        }

        public String Company_Code
        {
            get 
            { 
                return Encoding.ASCII.GetString(company_code); 
            }
            set 
            {
                company_code = Encoding.ASCII.GetBytes(value); ; 
            }
        }

        public string Serial_No
        {
            get 
            { 
                return Encoding.ASCII.GetString(serial_no); 
            }
            set 
            {
                serial_no = Encoding.ASCII.GetBytes(value); 
            }
        }


        #endregion

        public FirmwareInfo()
        {
            modelID = new byte[1];
            model = new byte[5];
            company_code = new byte[3];
            serial_no = new byte[8];
            date = new byte[9];
            time = new byte[6];
            release_id = new byte[6];
            version = new byte[6];
            info_1 = new byte[2];
            info_2 = new byte[2];
            rfu = new byte[2];
            features = new byte[12];
            dlms_version = new byte[4];
        }
    }

    public class FirmwareInfoHelper
    {
        public static FirmwareInfo Decode(byte[] byte_Array, int index, int length)
        {
            try
            {
                // <><><><><><><><><>><><><><><><><><><><><>><><><><><><><><><><><>><><><><><><><><><><><>><><>
                ///GET COMPLETE FIRMWARE INFO
                ///
                FirmwareInfo info_firmware = new FirmwareInfo();

                for (int i = 0; i < byte_Array.Length; i++)
                {
                    byte_Array[i] = (byte)(byte_Array[i] ^ (0x55));
                }
                int traverser = 0;

                info_firmware.modelID[0] = (byte_Array[traverser]);
                traverser++;


                for (int i = 0; i < info_firmware.Model.Length; i++)
                {
                    info_firmware.model[i] = byte_Array[traverser + i];

                }
                traverser += info_firmware.Model.Length;


                for (int i = 0; i < info_firmware.Company_Code.Length; i++)
                {
                    info_firmware.company_code[i] = byte_Array[traverser + i];

                }
                traverser += info_firmware.Company_Code.Length;


                for (int i = 0; i < info_firmware.Serial_No.Length; i++)
                {
                    info_firmware.serial_no[i] = byte_Array[traverser + i];
                }
                traverser += info_firmware.Serial_No.Length;


                for (int i = 0; i < info_firmware.date.Length; i++)
                {
                    info_firmware.date[i] = byte_Array[traverser + i];

                }
                traverser += info_firmware.date.Length;


                for (int i = 0; i < info_firmware.time.Length; i++)
                {
                    info_firmware.time[i] = byte_Array[traverser + i];

                }
                traverser += info_firmware.time.Length;

                for (int i = 0; i < info_firmware.release_id.Length; i++)
                {
                    info_firmware.release_id[i] = byte_Array[traverser + i];

                }
                traverser += info_firmware.release_id.Length;


                for (int i = 0; i < info_firmware.version.Length; i++)
                {
                    info_firmware.version[i] = byte_Array[traverser + i];

                }
                traverser += info_firmware.version.Length;


                for (int i = 0; i < info_firmware.info_1.Length; i++)
                {
                    info_firmware.info_1[i] = byte_Array[traverser + i];

                }
                traverser += info_firmware.info_1.Length;

                for (int i = 0; i < info_firmware.info_2.Length; i++)
                {
                    info_firmware.info_2[i] = byte_Array[traverser + i];

                }
                traverser += info_firmware.info_2.Length;


                for (int i = 0; i < info_firmware.rfu.Length; i++)
                {
                    info_firmware.rfu[i] = byte_Array[traverser + i];

                }
                traverser += info_firmware.rfu.Length;

                for (int i = 0; i < info_firmware.features.Length; i++)
                {
                    info_firmware.features[i] = byte_Array[traverser + i];

                }
                traverser += info_firmware.features.Length;

                for (int i = 0; i < info_firmware.dlms_version.Length; i++)
                {
                    info_firmware.dlms_version[i] = byte_Array[traverser + i];

                }
                return info_firmware;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while decoding Firmware Info", ex);
            }
        }
    }
}
