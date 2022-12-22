using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedCode.Comm.DataContainer
{
    public class FirmwareInfo
    {
        public byte[] modelID;
        public byte[] model;
        public byte[] company_code;
        public byte[] serial_no;
        public byte[] date;
        public byte[] time;
        public byte[] release_id;
        public byte[] version;
        public byte[] info_1;
        public byte[] info_2;
        public byte[] rfu;
        public byte[] features;
        public byte[] dlms_version;

        public string ModelID { get { return Encoding.ASCII.GetString(modelID); } }
        public string Model { get { return Encoding.ASCII.GetString(model); } }
        public string Company_code { get { return Encoding.ASCII.GetString(company_code); } }
        public string Serial_no { get { return Encoding.ASCII.GetString(serial_no); } }
        public string Date { get { return Encoding.ASCII.GetString(date); } }
        public string Time { get { return Encoding.ASCII.GetString(time); } }
        public string Release_id { get { return Encoding.ASCII.GetString(release_id); } }
        public string Version { get { return Encoding.ASCII.GetString(version); } }
        //public string Info_1;
        //public string Info_2;
        //public string Rfu;
        //public string Features;
        public string Dlms_version { get { return Encoding.ASCII.GetString(dlms_version); } }

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

        public void Decode(byte[] byte_Array)
        {
            for (int i = 0; i < byte_Array.Length; i++)
            {
                byte_Array[i] = (byte)(byte_Array[i] ^ (0x55));
            }
            int traverser = 0;

            this.modelID[0] = (byte_Array[traverser]);
            traverser++;


            for (int i = 0; i < this.model.Length; i++)
            {
                this.model[i] = byte_Array[traverser + i];

            }
            traverser += this.model.Length;


            for (int i = 0; i < this.company_code.Length; i++)
            {
                this.company_code[i] = byte_Array[traverser + i];

            }
            traverser += this.company_code.Length;


            for (int i = 0; i < this.serial_no.Length; i++)
            {
                this.serial_no[i] = byte_Array[traverser + i];
            }
            traverser += this.serial_no.Length;


            for (int i = 0; i < this.date.Length; i++)
            {
                this.date[i] = byte_Array[traverser + i];

            }
            traverser += this.date.Length;


            for (int i = 0; i < this.time.Length; i++)
            {
                this.time[i] = byte_Array[traverser + i];

            }
            traverser += this.time.Length;

            for (int i = 0; i < this.release_id.Length; i++)
            {
                this.release_id[i] = byte_Array[traverser + i];

            }
            traverser += this.release_id.Length;


            for (int i = 0; i < this.version.Length; i++)
            {
                this.version[i] = byte_Array[traverser + i];

            }
            traverser += this.version.Length;


            for (int i = 0; i < this.info_1.Length; i++)
            {
                this.info_1[i] = byte_Array[traverser + i];

            }
            traverser += this.info_1.Length;

            for (int i = 0; i < this.info_2.Length; i++)
            {
                this.info_2[i] = byte_Array[traverser + i];

            }
            traverser += this.info_2.Length;


            for (int i = 0; i < this.rfu.Length; i++)
            {
                this.rfu[i] = byte_Array[traverser + i];

            }
            traverser += this.rfu.Length;

            for (int i = 0; i < this.features.Length; i++)
            {
                this.features[i] = byte_Array[traverser + i];

            }
            traverser += this.features.Length;

            for (int i = 0; i < this.dlms_version.Length; i++)
            {
                this.dlms_version[i] = byte_Array[traverser + i];

            }
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


                for (int i = 0; i < info_firmware.Company_code.Length; i++)
                {
                    info_firmware.company_code[i] = byte_Array[traverser + i];

                }
                traverser += info_firmware.Company_code.Length;


                for (int i = 0; i < info_firmware.Serial_no.Length; i++)
                {
                    info_firmware.serial_no[i] = byte_Array[traverser + i];
                }
                traverser += info_firmware.Serial_no.Length;


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
