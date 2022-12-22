using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using comm;
using DLMS;
using GUI;
using SmartEyeControl_7.Controllers;

namespace SmartEyeControl_7.comm
{
    public class Monthly_Billing
    {
        public double KwhImport;
        public double KwhExport;
        public double KwhAbsolute;
        public double KvarhQ1;
        public double KvarhQ2;
        public double KvarhQ3;
        public double KvarhQ4;
        public double KvarhAbsolute;
        public double Kvah;
        public double TamperKwh;
        public double MdiKw;
        public double MdiKvar;
        public double PowerFactor;
        public double ThisMonthMdiKw;
        public double ThisMonthMdiKvar;

        public double Decode_Any(Base_Class arg, byte Class_ID)
        {
            try
            {
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                if (Class_ID == 4)
                {
                    Class_4 temp_obj = (Class_4)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
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
    public class Cumulative_Billing
    {
        public double KwhImport;
        public double KwhExport;
        public double KwhAbsolute;
        public double KvarhQ1;
        public double KvarhQ2;
        public double KvarhQ3;
        public double KvarhQ4;
        public double KvarhAbsolute;
        public double Kvah;
        public double TamperKwh;
        public double MdiKw;
        public double MdiKvar;
        public double PowerFactor;
        public double CurrentMonthMdiKw;
        public double CurrentMonthMdiKvar;

        public double Decode_Any(Base_Class arg, byte Class_ID)
        {
            try
            {
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        temp = double.NaN;
                    return temp;
                }
                if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        temp = double.NaN;
                    return temp;
                }
                if (Class_ID == 4)
                {
                    Class_4 temp_obj = (Class_4)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        temp = double.NaN;
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
