using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharedCode.Comm.Param
{
    public class Fuison_IO_StateList : ICustomStructure
    {
        #region Properties
        public List<Param_FusionIOState> IOStateList { get; set; }
        #endregion

        #region Constructor
        public Fuison_IO_StateList()
        {
            IOStateList = new List<Param_FusionIOState>();

            IOStateList.Add(new Param_FusionIOState(01, "MD ", 034, "P1.0"));
            IOStateList.Add(new Param_FusionIOState(02, "Switch Scroll", 035, "P1.1"));
            IOStateList.Add(new Param_FusionIOState(03, "LCD S37", 036, "P1.2"));
            IOStateList.Add(new Param_FusionIOState(04, "LCD S36", 037, "P1.3"));
            IOStateList.Add(new Param_FusionIOState(05, "LCD S35", 038, "P1.4"));
            IOStateList.Add(new Param_FusionIOState(06, "LCD S34", 039, "P1.5"));
            IOStateList.Add(new Param_FusionIOState(07, "LCD S33", 040, "P1.6"));
            IOStateList.Add(new Param_FusionIOState(08, "LCD S32", 041, "P1.7"));
            IOStateList.Add(new Param_FusionIOState(09, "TP21", 017, "P2.0"));
            IOStateList.Add(new Param_FusionIOState(10, "ADE IRQ", 018, "P2.1"));
            IOStateList.Add(new Param_FusionIOState(11, "Door Open", 019, "P2.2"));
            IOStateList.Add(new Param_FusionIOState(12, "RTC INT", 020, "P2.3"));
            IOStateList.Add(new Param_FusionIOState(13, "MODEM TX", 021, "P2.4"));
            IOStateList.Add(new Param_FusionIOState(14, "MODEM RX", 022, "P2.5"));
            IOStateList.Add(new Param_FusionIOState(15, "LCD R03", 023, "P2.6"));
            IOStateList.Add(new Param_FusionIOState(16, "LCD R13", 024, "P2.7"));
            IOStateList.Add(new Param_FusionIOState(17, "LCD S31", 042, "P3.0"));
            IOStateList.Add(new Param_FusionIOState(18, "LCD S30", 043, "P3.1"));
            IOStateList.Add(new Param_FusionIOState(19, "LCD S29", 044, "P3.2"));
            IOStateList.Add(new Param_FusionIOState(20, "LCD S28", 045, "P3.3"));
            IOStateList.Add(new Param_FusionIOState(21, "LCD S27", 046, "P3.4"));
            IOStateList.Add(new Param_FusionIOState(22, "LCD S26", 047, "P3.5"));
            IOStateList.Add(new Param_FusionIOState(23, "LCD S25", 048, "P3.6"));
            IOStateList.Add(new Param_FusionIOState(24, "LCD S24", 049, "P3.7"));
            IOStateList.Add(new Param_FusionIOState(25, "LCD S23", 050, "P4.0"));
            IOStateList.Add(new Param_FusionIOState(26, "LCD S22", 051, "P4.1"));
            IOStateList.Add(new Param_FusionIOState(27, "LCD S21", 052, "P4.2"));
            IOStateList.Add(new Param_FusionIOState(28, "LCD S20", 053, "P4.3"));
            IOStateList.Add(new Param_FusionIOState(29, "LCD S19", 054, "P4.4"));
            IOStateList.Add(new Param_FusionIOState(30, "LCD S18", 055, "P4.5"));
            IOStateList.Add(new Param_FusionIOState(31, "LCD S17", 056, "P4.6"));
            IOStateList.Add(new Param_FusionIOState(32, "LCD S16", 057, "P4.7"));
            IOStateList.Add(new Param_FusionIOState(33, "ADE DI", 009, "P5.0"));
            IOStateList.Add(new Param_FusionIOState(34, "ADE CS/CE", 010, "P5.1"));
            IOStateList.Add(new Param_FusionIOState(35, "LCD R23", 028, "P5.2"));
            IOStateList.Add(new Param_FusionIOState(36, "LCD S42/COM1", 031, "P5.3"));
            IOStateList.Add(new Param_FusionIOState(37, "LCD S41/COM2", 032, "P5.4"));
            IOStateList.Add(new Param_FusionIOState(38, "LCD S40/COM3", 033, "P5.5"));
            IOStateList.Add(new Param_FusionIOState(39, "ADE DO", 016, "P5.6"));
            IOStateList.Add(new Param_FusionIOState(40, "ADE SCLK", 088, "P5.7"));
            IOStateList.Add(new Param_FusionIOState(41, "Batt Load", 097, "p6.0"));
            IOStateList.Add(new Param_FusionIOState(42, "Batt Volt", 098, "p6.1"));
            IOStateList.Add(new Param_FusionIOState(43, "RL STATUS(CAP CHAR", 099, "P6.2"));
            IOStateList.Add(new Param_FusionIOState(44, "CONTACTOR OFF", 100, "P6.3"));
            IOStateList.Add(new Param_FusionIOState(45, "CONTACTOR ON", 001, "P6.4"));
            IOStateList.Add(new Param_FusionIOState(46, "HALL", 002, "P6.5"));
            IOStateList.Add(new Param_FusionIOState(47, "TAMPER", 003, "P6.6"));
            IOStateList.Add(new Param_FusionIOState(48, "I2C SDA", 004, "P6.7"));

            IOStateList.Add(new Param_FusionIOState(00, "-------", 000, "----"));
            IOStateList.Add(new Param_FusionIOState(00, "-------", 000, "----"));

            IOStateList.Add(new Param_FusionIOState(49, "XT2 IN", 084, "P7.2"));
            IOStateList.Add(new Param_FusionIOState(50, "XT2 OUT", 085, "P7.3"));
            IOStateList.Add(new Param_FusionIOState(51, "I2C SCL", 005, "P7.4"));
            IOStateList.Add(new Param_FusionIOState(52, "I2C WP (U4)", 006, "P7.5"));
            IOStateList.Add(new Param_FusionIOState(53, "I2C WP (U5)", 007, "P7.6"));
            IOStateList.Add(new Param_FusionIOState(54, "PUVCC", 008, "P7.7"));
            IOStateList.Add(new Param_FusionIOState(55, "LCD S15", 058, "P8.0"));
            IOStateList.Add(new Param_FusionIOState(56, "LCD S14", 059, "P8.1"));
            IOStateList.Add(new Param_FusionIOState(57, "IR TX", 060, "P8.2"));
            IOStateList.Add(new Param_FusionIOState(58, "IR RX", 061, "P8.3"));
            IOStateList.Add(new Param_FusionIOState(59, "LCD S11", 062, "P8.4"));
            IOStateList.Add(new Param_FusionIOState(60, "LCD S10", 065, "P8.5"));
            IOStateList.Add(new Param_FusionIOState(61, "LCD S09", 066, "P8.6"));
            IOStateList.Add(new Param_FusionIOState(62, "LCD S08", 067, "P8.7"));
            IOStateList.Add(new Param_FusionIOState(63, "LCD S07", 068, "P9.0"));
            IOStateList.Add(new Param_FusionIOState(64, "LCD S06", 069, "P9.1"));
            IOStateList.Add(new Param_FusionIOState(65, "LCD S05", 070, "P9.2"));
            IOStateList.Add(new Param_FusionIOState(66, "LCD S04", 071, "P9.3"));
            IOStateList.Add(new Param_FusionIOState(67, "LCD S03", 072, "P9.4"));
            IOStateList.Add(new Param_FusionIOState(68, "LCD S02", 073, "P9.5"));
            IOStateList.Add(new Param_FusionIOState(69, "LCD S01", 074, "P9.6"));
            IOStateList.Add(new Param_FusionIOState(70, "LCD S00", 075, "P9.7"));
            IOStateList.Add(new Param_FusionIOState(71, "PRG TDO", 092, "PJ.0"));
            IOStateList.Add(new Param_FusionIOState(72, "PRG TCLK", 093, "PJ.1"));
            IOStateList.Add(new Param_FusionIOState(73, "PRG TMS", 094, "PJ.2"));
            IOStateList.Add(new Param_FusionIOState(74, "PRG TCK", 095, "PJ.3"));
            IOStateList.Add(new Param_FusionIOState(75, "CALIB MODE", 077, "PU.0"));
            IOStateList.Add(new Param_FusionIOState(76, "Switch MDI RESET", 079, "PU.1"));
        }
        #endregion

        #region Encode/Decode
        public byte[] Encode_Data()
        {
            throw new NotImplementedException();
        }

        public void Decode_Data(byte[] Data)
        {
            int array_traverse = 0;
            Decode_Data(Data, ref array_traverse, Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse, int length)
        {

            int BitPos = 0;
            PropertyInfo[] properties = typeof(Register).GetProperties();
            int pInfoIndex = 0;
            for (int i = array_traverse; i < Data.Length;)
            {
                DecodeGeneric("IN", properties[pInfoIndex].Name, Data[i + 0], ref BitPos);
                DecodeGeneric("OUT", properties[pInfoIndex].Name, Data[i + 1], ref BitPos);
                DecodeGeneric("Direction", properties[pInfoIndex].Name, Data[i + 2], ref BitPos);
                DecodeGeneric("Selection", properties[pInfoIndex].Name, Data[i + 3], ref BitPos);
                DecodeGeneric("ResisterEnable", properties[pInfoIndex].Name, Data[i + 4], ref BitPos);
                i += 6;
                BitPos += 8;
                if ((i + 2) % (6 * 9 + 2) == 0) { i += 2; pInfoIndex++; BitPos = 0; }
            }
        }
        private void DecodeGeneric(string propertyName, string subPropertyName, byte data, ref int listIndex)
        {
            for (int bitIndex = 0; bitIndex < 8; bitIndex++)
            {
                if (listIndex + bitIndex >= IOStateList.Count) return;
                Param_FusionIOState IO_Obj = IOStateList[listIndex + bitIndex];
                foreach (PropertyInfo pi in IO_Obj.GetType().GetProperties())
                {
                    if (pi.Name.Equals(propertyName))// && pi.GetType() == typeof(Register))
                    {
                        Register reg_Obj = (Register)pi.GetValue(IO_Obj, null);
                        foreach (PropertyInfo sub_pi in reg_Obj.GetType().GetProperties())
                        {
                            if (sub_pi.Name.Equals(subPropertyName))
                            {
                                bool isHigh = ((data & (byte)Math.Pow(2, bitIndex)) == (byte)Math.Pow(2, bitIndex));
                                sub_pi.SetValue(reg_Obj, isHigh ? "1" : "0", null);
                                break;
                            }
                        }
                    }
                }
            }
            //listIndex += 8;
        }

        #endregion                                Switch MDI RESET

        #region Cloneable
        public object Clone()
        {
            throw new NotImplementedException();
        }
        #endregion

    }

    public class Param_FusionIOState
    {
        #region Properties
        public int SrNo { get; set; }
        public string Name { get; set; }
        public byte PIN_NO { get; set; }
        public string PORT { get; set; }
        public Register IN { get; set; }
        public Register Direction { get; set; }
        public Register OUT { get; set; }
        public Register Selection { get; set; }
        public Register ResisterEnable { get; set; }
        #endregion

        #region Constructor
        public Param_FusionIOState(int srno, string name, byte pinNo, string port)
        {
            this.SrNo = srno;
            this.Name = name;
            this.PIN_NO = pinNo;
            this.PORT = port;
            Direction = new Register(OutPutType.IN_OUT);
            IN = new Register(OutPutType.PLAIN);
            OUT = new Register(OutPutType.PLAIN);
            Selection = new Register(OutPutType.IO_PPH);
            ResisterEnable = new Register(OutPutType.PLAIN);

        }
        #endregion

    }

    public class Register
    {
        #region Members
        private string _systemResetState;
        private string _powerMode;
        private string _lowPowerMode;
        private OutPutType _outputType;
        #endregion

        #region Properties
        public string SystemResetState
        {
            get { return Translate(_systemResetState); }
            set { _systemResetState = value; }
        }
        public string ActivePowerMode
        {
            get
            {
                if (_powerMode == _systemResetState) return string.Empty;
                else return Translate(_powerMode);
            }
            set
            {
                _powerMode = value;
            }
        }
        public string LowPowerMode
        {
            get
            {
                if (_lowPowerMode == _systemResetState) return string.Empty;
                else return Translate(_lowPowerMode);
            }
            set
            {
                _lowPowerMode = value;
            }
        }
        #endregion

        #region Constructor
        public Register(OutPutType outputType)
        {
            _systemResetState = "0";
            _powerMode = "0";
            _lowPowerMode = "0";
            _outputType = outputType;
        }
        #endregion

        #region METHOD
        private string Translate(string input)
        {
            string result = input;
            if (_outputType == OutPutType.IN_OUT)
            {
                result = (input == "0" ? "INPUT" : "OUTPUT");
            }
            else if (_outputType == OutPutType.IO_PPH)
            {
                result = (input == "0" ? "IO" : "PPH");
            }
            return result;
        }
        #endregion

    }

    public enum OutPutType
    {
        IN_OUT,
        IO_PPH,
        PLAIN
    }
}
