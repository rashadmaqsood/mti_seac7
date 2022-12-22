using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using DLMS.Comm;

namespace SharedCode.Comm.DataContainer
{
    public class InstantaneousObjClass
    {
        public string MSN;
        public StDateTime dateTime;
        public double current_PhaseA;
        public double current_PhaseB;
        public double current_PhaseC;
        public double current_PhaseTL;
        public double voltage_PhaseA;
        public double voltage_PhaseB;
        public double voltage_PhaseC;
        public double voltage_PhaseTL;
        public double active_powerPositive_PhaseA;
        public double active_powerPositive_PhaseB;
        public double active_powerPositive_PhaseC;
        public double active_powerPositive_PhaseTL;
        public double active_powerNegative_PhaseA;
        public double active_powerNegative_PhaseB;
        public double active_powerNegative_PhaseC;
        public double active_powerNegative_PhaseTL;
        public double reactive_powerPositive_PhaseA;
        public double reactive_powerPositive_PhaseB;
        public double reactive_powerPositive_PhaseC;
        public double reactive_powerPositive_PhaseTL;
        public double reactive_powerNegative_PhaseA;
        public double reactive_powerNegative_PhaseB;
        public double reactive_powerNegative_PhaseC;
        public double reactive_powerNegative_PhaseTL;
        public double frequency;

        public double Decode_Any(Base_Class arg, byte Class_ID)
        {
            try
            {
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);

                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        temp = double.NaN;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        temp = double.PositiveInfinity;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        temp = double.NegativeInfinity;
                    return temp;
                }
                if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        temp = double.NaN;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        temp = double.PositiveInfinity;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        temp = double.NegativeInfinity;
                    return temp;


                    return temp;
                }
                if (Class_ID == 4)
                {
                    Class_4 temp_obj = (Class_4)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        temp = double.NaN;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        temp = double.PositiveInfinity;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        temp = double.NegativeInfinity;
                    return temp;
                }
                if (Class_ID == 5)
                {

                    Class_5 temp_obj = (Class_5)arg;

                    double temp = Convert.ToDouble(temp_obj.periodCount);
                    if (temp_obj.GetAttributeDecodingResult(0x09) == DecodingResult.NoAccess)
                        temp = double.NaN;
                    else if (temp_obj.GetAttributeDecodingResult(0x09) == DecodingResult.DataNotPresent)
                        temp = double.PositiveInfinity;
                    else if (temp_obj.GetAttributeDecodingResult(0x09) != DecodingResult.Ready)
                        temp = double.NegativeInfinity;
                    return temp;
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
